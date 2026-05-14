using CommonLibraryP.MachinePKG;
using FPC.Data;
using System.Collections.Concurrent;

namespace FPC.Services
{
    /// <summary>
    /// 機台標籤服務 - 負責讀取捲繞機訊號資料
    /// </summary>
    public class MachineTagService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MachineTagService> _logger;
        private readonly MachineService _machineService;

        public MachineTagService(IServiceProvider serviceProvider, ILogger<MachineTagService> logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _machineService = _serviceProvider.GetRequiredService<MachineService>();
        }

        /// <summary>
        /// 取得單一捲繞機的訊號資料
        /// </summary>
        /// <param name="machineName">機台名稱</param>
        /// <param name="winderName">捲繞機名稱 (例: Winder_001)</param>
        /// <returns>捲繞機訊號資料</returns>
        public async Task<WinderSignals> GetWinderSignalsAsync(string machineName, string winderName)
        {
            if (string.IsNullOrWhiteSpace(machineName))
                throw new ArgumentException("機台名稱不可為空", nameof(machineName));

            if (string.IsNullOrWhiteSpace(winderName))
                throw new ArgumentException("捲繞機名稱不可為空", nameof(winderName));

            try
            {
                var result = new WinderSignals();

                // Prefix: Winder_001 / Winder_002 / Winder_300
                string prefix = winderName;

                result.WinderNumber = await ReadTagAsync(machineName, $"{prefix}_捲繞機編號");
                result.Status = await ReadTagAsync(machineName, $"{prefix}_狀態");
                result.Reserved1 = await ReadTagAsync(machineName, $"{prefix}_保留1");
                result.Reserved2 = await ReadTagAsync(machineName, $"{prefix}_保留2");
                result.DoffingCount = await ReadTagAsync(machineName, $"{prefix}_落筒次數");
                result.YarnLength = await ReadTagAsync(machineName, $"{prefix}_紗長");
                result.RearYarnLength_CAX_2COP = await ReadTagAsync(machineName, $"{prefix}_後紗長_CAX_2COP");
                result.WindingTime = await ReadTagAsync(machineName, $"{prefix}_捲繞時間");
                result.WindingTime_CAX_2COP = await ReadTagAsync(machineName, $"{prefix}_捲繞時間_CAX_2COP");
                result.Reserved3 = await ReadTagAsync(machineName, $"{prefix}_保留3");
                result.WaitingSpindleYarnLength_CAX = await ReadTagAsync(machineName, $"{prefix}_待機軸紗長_CAX");
                result.WaitingSpindleYarnLength_CAX_2COP = await ReadTagAsync(machineName, $"{prefix}_待機軸紗長_CAX_2COP");
                result.WaitingSpindleWindingTime_CAX = await ReadTagAsync(machineName, $"{prefix}_待機軸捲繞時間_CAX");
                result.WaitingSpindleWindingTime_CAX_2COP = await ReadTagAsync(machineName, $"{prefix}_待機軸捲繞時間_CAX_2COP");
                result.Reserved4 = await ReadTagAsync(machineName, $"{prefix}_保留4");
                result.WindingRatioSetValue = await ReadTagAsync(machineName, $"{prefix}_捲繞比設定值");
                result.TurretAngle = await ReadTagAsync(machineName, $"{prefix}_轉塔角度");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "讀取捲繞機 {WinderName} 訊號時發生錯誤", winderName);
                throw;
            }
        }

        /// <summary>
        /// 一次取得所有捲繞機訊號 (1~300) - 並行處理版本
        /// </summary>
        /// <param name="machineName">機台名稱</param>
        /// <param name="maxDegreeOfParallelism">最大並行度 (預設 10)</param>
        /// <returns>所有捲繞機訊號列表</returns>
        public async Task<List<WinderSignals>> GetAllWinderSignalsAsync(
            string machineName, 
            int maxDegreeOfParallelism = 10)
        {
            if (string.IsNullOrWhiteSpace(machineName))
                throw new ArgumentException("機台名稱不可為空", nameof(machineName));

            _logger.LogInformation("開始讀取所有捲繞機訊號,機台: {MachineName}", machineName);

            var results = new ConcurrentBag<(int Index, WinderSignals Signals)>();
            var options = new ParallelOptions 
            { 
                MaxDegreeOfParallelism = maxDegreeOfParallelism 
            };

            try
            {
                await Parallel.ForEachAsync(
                    Enumerable.Range(1, 300),
                    options,
                    async (i, cancellationToken) =>
                    {
                        try
                        {
                            string winderName = $"Winder_{i:D3}";
                            var signals = await GetWinderSignalsAsync(machineName, winderName);
                            results.Add((i, signals));
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "讀取 Winder_{Index:D3} 時發生錯誤", i);
                        }
                    });

                var sortedList = results
                    .OrderBy(x => x.Index)
                    .Select(x => x.Signals)
                    .ToList();

                _logger.LogInformation("成功讀取 {Count} 個捲繞機訊號", sortedList.Count);
                return sortedList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "讀取所有捲繞機訊號時發生嚴重錯誤");
                throw;
            }
        }

        // ---------------------------------------------------------
        // 內部共用方法：讀取單一 Tag
        // ---------------------------------------------------------
        private async Task<string> ReadTagAsync(string machineName, string tagName)
        {
            try
            {
                var tag = await GetMachineTagAsync(machineName, tagName);
                return tag?.ValueString ?? string.Empty;
            }
            catch (Exception ex)
            {
                //_logger.LogWarning(ex, "讀取標籤 {TagName} 失敗,返回空字串", tagName);
                return string.Empty;
            }
        }

        // ---------------------------------------------------------
        // 你原本已有的 GetMachineTag
        // ---------------------------------------------------------
        public async Task<Tag> GetMachineTagAsync(string machineName, string tagName)
        {
            if (string.IsNullOrWhiteSpace(machineName))
                throw new ArgumentException("機台名稱不可為空", nameof(machineName));

            if (string.IsNullOrWhiteSpace(tagName))
                throw new ArgumentException("標籤名稱不可為空", nameof(tagName));

            try
            {
                var machineTag = await _machineService.GetMachineTag(machineName, tagName);

                return machineTag 
                    ?? throw new InvalidOperationException(
                        $"機台 '{machineName}' 中找不到標籤 '{tagName}'");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "取得機台標籤失敗 - 機台: {MachineName}, 標籤: {TagName}", 
                //    machineName, tagName);
                throw;
            }
        }
    }
}
