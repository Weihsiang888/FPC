# Logo 改為文字顯示更新說明

## 📋 更新內容

### ✅ 已完成的變更

#### 1. Header 導航列品牌顯示
**原設計：**
```html
<img src="images/logo.svg" alt="Techman Robot" />
```

**新設計：**
```html
<NavLink href="/" class="tm-brand-link">
	<span class="tm-brand-text">Techman Robot</span>
</NavLink>
```

**樣式特點：**
- 字體大小：24px
- 字重：粗體 (700)
- 顏色：淺色 `#D0D0CE`
- Hover 效果：變為智慧綠 `#89D926`
- 可點擊回到首頁

---

#### 2. Footer 頁尾品牌顯示
**原設計：**
```html
<img src="images/logo.svg" alt="Techman Robot" class="tm-footer-logo" />
```

**新設計：**
```html
<h3 class="tm-h3 tm-footer-brand">Techman Robot</h3>
```

**樣式特點：**
- 使用 H3 標籤語意化
- 字體大小：32px (var(--tm-font-size-h3))
- 字重：粗體
- 顏色：淺色 `#D0D0CE`
- 保持與原 Logo 相似的視覺權重

---

## 🎨 視覺效果對比

### Header 品牌區
| 項目 | 原設計 | 新設計 |
|------|--------|--------|
| 類型 | SVG 圖片 | 文字 |
| 高度 | 40px | 自適應 (24px 字體) |
| 顏色 | 白色濾鏡 | 淺灰 #D0D0CE |
| Hover | 無 | 智慧綠 #89D926 |
| 可點擊 | 否 | 是（回首頁） |

### Footer 品牌區
| 項目 | 原設計 | 新設計 |
|------|--------|--------|
| 類型 | SVG 圖片 | H3 文字 |
| 高度 | 48px | 自適應 (32px 字體) |
| 顏色 | 白色濾鏡 | 淺灰 #D0D0CE |
| 語意 | 圖片 | 標題標籤 |

---

## 📂 修改的檔案

### 1. **FPC/Components/Layout/MainLayout.razor**
**Header 品牌區域：**
```razor
<div class="tm-header-logo">
	<NavLink href="/" class="tm-brand-link">
		<span class="tm-brand-text">Techman Robot</span>
	</NavLink>
</div>
```

**Footer 品牌區域：**
```razor
<div class="tm-footer-left">
	<h3 class="tm-h3 tm-footer-brand">Techman Robot</h3>
	<div class="tm-footer-info">
		...
	</div>
</div>
```

### 2. **FPC/Components/Layout/MainLayout.razor.css**

**新增 Header 品牌樣式：**
```css
.tm-header-logo {
	display: flex;
	align-items: center;
}

.tm-brand-link {
	text-decoration: none;
	display: flex;
	align-items: center;
}

.tm-brand-text {
	font-size: 24px;
	font-weight: var(--tm-font-weight-bold);
	color: var(--tm-text-light);
	letter-spacing: -0.5px;
	transition: color 0.2s ease;
}

.tm-brand-link:hover .tm-brand-text {
	color: var(--tm-secondary);
}
```

**新增 Footer 品牌樣式：**
```css
.tm-footer-brand {
	color: var(--tm-text-light);
	font-weight: var(--tm-font-weight-bold);
	margin-bottom: var(--tm-spacing-xs);
	letter-spacing: -0.5px;
}
```

---

## ✨ 優勢與好處

### 1. **效能優化**
- ✅ 減少圖片資源載入
- ✅ 文字渲染比 SVG 更快
- ✅ 不需要額外的 HTTP 請求

### 2. **可訪問性提升**
- ✅ 文字天生具有更好的可讀性
- ✅ 屏幕閱讀器可以直接讀取
- ✅ 搜尋引擎更容易索引

### 3. **響應式友好**
- ✅ 文字自動適應不同解析度
- ✅ 在高 DPI 螢幕上更清晰
- ✅ 不會出現模糊或像素化

### 4. **維護簡便**
- ✅ 不需要管理圖片檔案
- ✅ 顏色與字體統一管理
- ✅ 易於修改品牌名稱

### 5. **互動增強**
- ✅ Header 品牌文字可點擊回首頁
- ✅ Hover 效果提升使用者體驗
- ✅ 視覺回饋更直觀

---

## 🎯 設計考量

### 字體選擇
- 使用全域定義的 `Montserrat` 字體家族
- 粗體 (700) 確保視覺權重
- 字母間距 `-0.5px` 使文字更緊湊專業

### 顏色策略
- **預設狀態**：淺灰色 `#D0D0CE` 與導航文字一致
- **Hover 狀態**：智慧綠 `#89D926` 符合品牌色系
- 與深色背景形成良好對比

### 尺寸比例
- Header: 24px 適合導航列
- Footer: 32px (H3) 與區塊視覺權重匹配

---

## 🔍 瀏覽器相容性

✅ 所有現代瀏覽器完全支援
- Chrome 90+
- Firefox 88+
- Safari 14+
- Edge 90+

---

## 📱 響應式表現

### 桌面版 (1920px+)
- Header 品牌文字 24px 清晰易讀
- Footer 品牌文字 32px 視覺突出

### 平板版 (768px - 1024px)
- 文字自動適應，無需調整
- 保持良好的可讀性

### 手機版 (< 768px)
- 文字不會失真或模糊
- 點擊區域適中易操作

---

## 🚀 未來擴展建議

### 1. 品牌副標題（可選）
```html
<div class="tm-brand">
	<span class="tm-brand-text">Techman Robot</span>
	<span class="tm-brand-subtitle">智慧製造</span>
</div>
```

### 2. 品牌圖示 + 文字組合（可選）
```html
<NavLink href="/" class="tm-brand-link">
	<span class="tm-brand-icon">🤖</span>
	<span class="tm-brand-text">Techman Robot</span>
</NavLink>
```

### 3. 多語言支援
```razor
@if (CurrentLanguage == "en")
{
	<span class="tm-brand-text">Techman Robot</span>
}
else
{
	<span class="tm-brand-text">達明機器人</span>
}
```

---

## ✅ 變更清單

- [x] 移除 Header SVG Logo
- [x] 新增 Header 品牌文字與樣式
- [x] 新增 Header 品牌文字 Hover 效果
- [x] 新增 Header 品牌文字點擊回首頁功能
- [x] 移除 Footer SVG Logo
- [x] 新增 Footer 品牌文字標題
- [x] 更新 CSS 移除圖片相關樣式
- [x] 新增品牌文字專屬樣式
- [x] 通過建置測試

---

**✅ 所有變更已完成並通過建置測試！**

現在的品牌顯示完全使用文字，更加簡潔、高效且易於維護。
