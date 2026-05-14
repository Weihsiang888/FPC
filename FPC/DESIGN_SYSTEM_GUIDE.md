# Techman Robot 設計系統組件使用指南

## 📚 目錄
- [全域設計變數](#全域設計變數)
- [TmButton 按鈕組件](#tmbutton-按鈕組件)
- [TmInput 輸入框組件](#tminput-輸入框組件)
- [TmCard 卡片組件](#tmcard-卡片組件)
- [佈局規範](#佈局規範)

---

## 🎨 全域設計變數

所有設計變數已在 `wwwroot/css/app.css` 中定義，請使用 CSS 變數而非硬編碼值。

### 色彩系統
```css
var(--tm-primary)           /* 品牌主色 #1D7A99 */
var(--tm-primary-hover)     /* 主色懸停 #1A6D89 */
var(--tm-secondary)         /* 品牌輔色 #89D926 */
var(--tm-secondary-hover)   /* 輔色懸停 #7EC823 */
var(--tm-error)             /* 錯誤紅 #D01717 */
var(--tm-bg-light)          /* 淺色背景 #FAFAFA */
var(--tm-bg-dark)           /* 深色背景 #2A2A2A */
var(--tm-text-main)         /* 主要文字 #252424 */
var(--tm-text-secondary)    /* 次要文字 #313233 */
var(--tm-text-light)        /* 淺色文字 #D0D0CE */
var(--tm-border-light)      /* 淺色框線 #EDEDED */
var(--tm-border-main)       /* 主要框線 #878A8C */
```

### 間距規範 (4px Base)
```css
var(--tm-spacing-xs)        /* 8px */
var(--tm-spacing-sm)        /* 16px */
var(--tm-spacing-md)        /* 24px */
var(--tm-spacing-lg)        /* 40px */
var(--tm-spacing-xl)        /* 60px */
var(--tm-spacing-xxl)       /* 80px */
var(--tm-spacing-hero)      /* 120px */
```

### 字型排版
```css
var(--tm-font-size-h1)      /* 48px */
var(--tm-font-size-h2)      /* 40px */
var(--tm-font-size-h3)      /* 32px */
var(--tm-font-size-body)    /* 16px */
var(--tm-font-size-subtitle)/* 14px */
var(--tm-font-size-caption) /* 12px */
```

---

## 🔘 TmButton 按鈕組件

### 基本用法
```razor
<TmButton ButtonType="Primary" Text="主要按鈕" />
<TmButton ButtonType="Secondary" Text="次要按鈕" />
```

### 完整參數範例
```razor
<TmButton 
	ButtonType="Primary"        @* Primary / Secondary *@
	Size="Large"                @* Default / Large *@
	Text="點擊我"
	IconClass="icon-class"      @* 可選 Icon *@
	IsEnabled="true"            @* true / false *@
	OnClick="HandleClick"       @* 點擊事件 *@
	CssClass="custom-class"     @* 自訂樣式類別 *@
	ButtonHtmlType="button"     @* button / submit / reset *@
/>
```

### 使用場景
```razor
@* 主要動作 *@
<TmButton ButtonType="Primary" Size="Large" Text="提交" OnClick="Submit" />

@* 次要動作 *@
<TmButton ButtonType="Secondary" Text="取消" OnClick="Cancel" />

@* 只有 Icon 的按鈕 *@
<TmButton ButtonType="Primary" IconClass="icon-search" />

@* 禁用狀態 *@
<TmButton ButtonType="Primary" Text="已禁用" IsEnabled="false" />
```

### 樣式特點
- **微圓角**：4px border-radius，拒絕膠囊形狀
- **高度**：Default 40px, Large 52px
- **Primary**：藍底白字，Hover 時上移並顯示陰影
- **Secondary**：白底藍框，Hover 時轉為藍底白字

---

## 📝 TmInput 輸入框組件

### 基本用法
```razor
<TmInput 
	Label="使用者名稱" 
	Placeholder="請輸入使用者名稱" 
	@bind-Value="username" 
/>
```

### 完整參數範例
```razor
<TmInput 
	Label="電子郵件"
	Type="email"                @* text / password / email / number 等 *@
	Placeholder="請輸入電子郵件"
	@bind-Value="email"
	PrefixIcon="icon-mail"      @* 前綴圖示 *@
	SuffixIcon="icon-check"     @* 後綴圖示 *@
	ErrorMessage="@errorMsg"    @* 錯誤訊息 *@
	HelperText="範例：user@example.com"  @* 輔助說明 *@
	Disabled="false"
	CssClass="custom-input"
	OnFocus="HandleFocus"
	OnBlur="HandleBlur"
/>
```

### 使用場景
```razor
@* 帶標籤的輸入框 *@
<TmInput Label="姓名" Placeholder="請輸入姓名" @bind-Value="name" />

@* 密碼輸入 *@
<TmInput Label="密碼" Type="password" @bind-Value="password" />

@* 帶驗證錯誤的輸入 *@
<TmInput 
	Label="電子郵件" 
	@bind-Value="email" 
	ErrorMessage="@(string.IsNullOrEmpty(email) ? "此欄位必填" : "")" 
/>

@* 帶前綴 Icon *@
<TmInput 
	Label="搜尋" 
	PrefixIcon="icon-search" 
	Placeholder="搜尋內容..." 
	@bind-Value="searchTerm" 
/>
```

### 樣式特點
- **固定高度**：40px
- **Focus 狀態**：框線轉為品牌藍，顯示藍色陰影
- **錯誤狀態**：紅色框線，下方顯示紅色錯誤訊息（12px）
- **禁用狀態**：灰色背景，不可點擊

---

## 📦 TmCard 卡片組件

### 基本用法
```razor
<TmCard Title="卡片標題" Description="卡片描述">
	<p>這是卡片的內容區域</p>
</TmCard>
```

### 完整參數範例
```razor
<TmCard 
	Title="功能卡片"
	Description="功能說明文字"
	IconClass="icon-feature"    @* Header Icon *@
	CardStyle="Hover"           @* Default / Hover / Dark *@
	Clickable="true"            @* 是否可點擊 *@
	OnClick="HandleCardClick"
	CssClass="custom-card"
>
	<HeaderContent>
		<TmButton ButtonType="Secondary" Text="操作" />
	</HeaderContent>

	<ChildContent>
		<p class="tm-body">主要內容區域</p>
	</ChildContent>

	<FooterContent>
		<div style="display: flex; justify-content: space-between;">
			<span>Footer 左側</span>
			<span>Footer 右側</span>
		</div>
	</FooterContent>
</TmCard>
```

### 使用場景
```razor
@* 簡單資訊卡片 *@
<TmCard Title="統計數據" Description="今日數據統計">
	<h3 class="tm-h3">1,234</h3>
</TmCard>

@* 可懸停互動卡片 *@
<TmCard Title="功能模組" CardStyle="Hover" Clickable="true" OnClick="NavigateToModule">
	<p>點擊進入功能模組</p>
</TmCard>

@* 深色背景卡片 *@
<TmCard Title="夜間模式" CardStyle="Dark">
	<p>深色主題內容</p>
</TmCard>

@* 帶 Footer 的卡片 *@
<TmCard Title="計數器">
	<ChildContent>
		<h2 class="tm-h2">@count</h2>
	</ChildContent>
	<FooterContent>
		<TmButton ButtonType="Primary" Text="增加" OnClick="Increment" />
	</FooterContent>
</TmCard>
```

### 樣式特點
- **極簡風格**：淺色框線 `var(--tm-border-light)`
- **輕淡陰影**：`box-shadow: 0 4px 12px rgba(0,0,0,0.05)`
- **微圓角**：8px border-radius
- **Hover 變體**：懸停時上移 4px，顯示較深陰影，框線變藍

---

## 📐 佈局規範

### MainLayout 結構
```
┌─────────────────────────────────┐
│  Header (固定頂部)               │ 72px
├─────────────────────────────────┤
│                                 │
│  Main Content (彈性區域)         │
│  └── 背景裝飾光斑                │
│                                 │
├─────────────────────────────────┤
│  Footer (深色背景)               │ 自適應
└─────────────────────────────────┘
```

### 頁面容器標準
```razor
<div class="page-container">
	<div class="page-header">
		<h1 class="tm-h1">頁面標題</h1>
		<p class="tm-body" style="color: var(--tm-text-secondary);">頁面描述</p>
	</div>

	<div class="page-content">
		<!-- 主要內容 -->
	</div>
</div>
```

```css
.page-container {
	padding: var(--tm-spacing-lg) var(--tm-spacing-md);
	max-width: 1200px;
	margin: 0 auto;
}

.page-header {
	margin-bottom: var(--tm-spacing-xl);
	text-align: center;
}
```

### 背景裝飾光斑
```razor
<!-- 在 Main Content 中加入 -->
<div class="tm-slice tm-slice-blue" style="width: 600px; height: 600px; top: 10%; left: -10%;"></div>
<div class="tm-slice tm-slice-green" style="width: 500px; height: 500px; top: 50%; right: -5%;"></div>
```

---

## ⚠️ 重要規範

### ❌ 禁止事項
1. **禁止硬編碼顏色**：必須使用 CSS 變數
2. **禁止大圓角膠囊**：統一使用 4px 或 8px 圓角
3. **禁止紫色或漸層色塊**：只使用品牌色（藍、綠）
4. **禁止不符合 4px 倍數的間距**

### ✅ 正確做法
1. 所有顏色使用 `var(--tm-*)`
2. 間距使用 `var(--tm-spacing-*)`
3. 字型大小使用 `var(--tm-font-size-*)`
4. 使用 `gap` 屬性控制組件間距
5. 標題使用 `.tm-h1` ~ `.tm-h5` 類別或對應 HTML 標籤

---

## 🎯 快速範例：完整表單

```razor
<TmCard Title="使用者註冊">
	<ChildContent>
		<div style="display: flex; flex-direction: column; gap: var(--tm-spacing-sm);">
			<TmInput Label="使用者名稱" 
					 Placeholder="請輸入使用者名稱" 
					 @bind-Value="username" 
					 ErrorMessage="@usernameError" />

			<TmInput Label="電子郵件" 
					 Type="email" 
					 Placeholder="user@example.com" 
					 @bind-Value="email" 
					 ErrorMessage="@emailError" />

			<TmInput Label="密碼" 
					 Type="password" 
					 Placeholder="至少 8 個字元" 
					 @bind-Value="password" 
					 ErrorMessage="@passwordError" />
		</div>
	</ChildContent>

	<FooterContent>
		<div style="display: flex; gap: var(--tm-spacing-sm); justify-content: flex-end;">
			<TmButton ButtonType="Secondary" Text="取消" OnClick="Cancel" />
			<TmButton ButtonType="Primary" Text="註冊" OnClick="Register" />
		</div>
	</FooterContent>
</TmCard>
```

---

## 📞 聯繫與支援

如有組件使用問題或建議，請聯繫開發團隊。

**© 2024 Techman Robot Inc.** - 協作機器人領導品牌
