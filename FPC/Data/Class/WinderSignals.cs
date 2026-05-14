namespace FPC.Data
{
    /// <summary>
    /// 捲繞機訊號資料模型
    /// </summary>
    public class WinderSignals
    {
        /// <summary>捲繞機編號</summary>
        public string WinderNumber { get; set; } = string.Empty;

        /// <summary>狀態</summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>保留欄位 1</summary>
        public string Reserved1 { get; set; } = string.Empty;

        /// <summary>保留欄位 2</summary>
        public string Reserved2 { get; set; } = string.Empty;

        /// <summary>落筒次數</summary>
        public string DoffingCount { get; set; } = string.Empty;

        /// <summary>紗長</summary>
        public string YarnLength { get; set; } = string.Empty;

        /// <summary>後紗長 (CAX 2COP)</summary>
        public string RearYarnLength_CAX_2COP { get; set; } = string.Empty;

        /// <summary>捲繞時間</summary>
        public string WindingTime { get; set; } = string.Empty;

        /// <summary>捲繞時間 (CAX 2COP)</summary>
        public string WindingTime_CAX_2COP { get; set; } = string.Empty;

        /// <summary>保留欄位 3</summary>
        public string Reserved3 { get; set; } = string.Empty;

        /// <summary>待機軸紗長 (CAX)</summary>
        public string WaitingSpindleYarnLength_CAX { get; set; } = string.Empty;

        /// <summary>待機軸紗長 (CAX 2COP)</summary>
        public string WaitingSpindleYarnLength_CAX_2COP { get; set; } = string.Empty;

        /// <summary>待機軸捲繞時間 (CAX)</summary>
        public string WaitingSpindleWindingTime_CAX { get; set; } = string.Empty;

        /// <summary>待機軸捲繞時間 (CAX 2COP)</summary>
        public string WaitingSpindleWindingTime_CAX_2COP { get; set; } = string.Empty;

        /// <summary>保留欄位 4</summary>
        public string Reserved4 { get; set; } = string.Empty;

        /// <summary>捲繞比設定值</summary>
        public string WindingRatioSetValue { get; set; } = string.Empty;

        /// <summary>轉塔角度</summary>
        public string TurretAngle { get; set; } = string.Empty;
    }
}
