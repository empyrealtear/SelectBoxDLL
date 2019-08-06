## **[SelectBox.dll] Help**
> 此文为 `SelectBox.dll` 在 `powershell` 中的使用方案, 个人没有完全理解到 `powershell二进制模块` 的制作方法。因此, 采用 `C#` 方法传参的方式, 进行快速将参数传入 dll 函数中。

## **Content**
--------------

<!-- vim-markdown-toc GFM -->
- [**类方法 Class Help**](#class-help)
  - [1. 框架设置 BoxSetting](#1-boxsetting)
  - [2. 框架绘制 BoxWrite  ](#2-boxwrite)
  - [3. 静态方法 Function  ](#3-function)
- [**枚举器 Eumn Help**](#eumn-help)
  - [1. 对齐枚举 AlignmentEnum](#1-alignmentenum)
  - [2. 框线枚举 PreBorderEnum](#2-preborderenum)
- [**结构体 Struct Help**](#struct-help)
  - [1. 坐标锚点 Position](#1-position)
  - [2. 字色填充 Colors](#2-colors)
  - [3. 空白设置 MarginPaddingObj](#3-marginpaddingobj)
  - [4. 框线设置 BorderObj](#4-borderobj)
  - [5. 内容字符 ContentObj](#5-contentobj)
  - [6. 内容设置 ContentSetObj](#6-contentsetobj)
  - [7. 画布颜色 GraphColorsObj](#7-graphcolorsobj)
  - [8. 选项读取 ReadContentObj](#8-readcontentobj)
<!-- vim-markdown-toc -->

--------------

### **FIXME**
```powershell
$BoxSetting.ContentSet = [SelectBoxDll.ContentSetObj]::new($Strings.Count, [int[]](1), $true, [SelectBoxDll.AlignmentEnum]::Left)
# FIXME: 当按列排序时, 可能会出现不同列数对应同一行数, 这些列数是无效的值
# HACK:  当列数设置出于无效值时, 可转而设置行数

$BoxSetting.Border = [SelectBoxDll.BorderObj]::new("", "", "", "", "", "") # 等价于未设置
$BoxSetting.Border = [SelectBoxDll.BorderObj]::new("", "", "", "", "", "", "", "") # 下边界为空时, 报错
# FIXME: 上下边界为空或空字符串时, 上下边界报错
# HACK:  计算上下边界的绘制长度时, 使用了上下边界长度作为被除数, 若要改动比较困难, 暂且将上边界的最小长度改为了 1
```

### **Class Help**
------------------

#### 1. BoxSetting
* **1. 外部传参**
```powershell
$BoxSetting = New-Object SelectBoxDll.BoxSetting
# public void NewSet(Boolean oldconsole, Position pos)
$BoxSetting.NewSet($false, [SelectBoxDll.Position]::new($true))

# public void NewSet(Boolean oldconsole, Position pos,
#                    MarginPaddingObj marginobj, MarginPaddingObj paddingobj,
#                    BorderObj borderobj, ContentObj contentobj, ContentSetObj contentsetobj)
## 局部设置
$BoxSetting.Content = [SelectBoxDll.ContentObj]::new("> ", $Strings)

## 全局设置
$Margin     = [SelectBoxDll.MarginPaddingObj]::new(2, 1)
$Padding    = [SelectBoxDll.MarginPaddingObj]::new(2, 0)
$Border     = [SelectBoxDll.BorderObj]::new([SelectBoxDll.PreBorderEnum]::AllThinLine)
$Content    = [SelectBoxDll.ContentObj]::new("> ",$Strings)
$ContentSet = $ContentSet = [SelectBoxDll.ContentSetObj]::new(3, [int[]](1), $false, [SelectBoxDll.AlignmentEnum]::Left)

$BoxSetting.NewSet($false, [SelectBoxDll.Position]::new($true), $Margin, $Padding, $Border, $Content, $ContentSet)
```

| Container | Type | Transferable | Resume
|-----------|------|:------------:|-------
| `Margin`     | MarginPaddingObj | Yes | 框外空白
| `Border`     | BorderObj        | Yes | 整体框线
| `Padding`    | MarginPaddingObj | Yes | 框内空白
| `Content`    | ContentObj       | Yes | 内容字符
| `ContentSet` | ContentSetObj    | Yes | 内容设置
| `Position`   | Position         | Yes | 坐标锚点
| `OldConsole` | Boolean          | Yes | 旧控制台模式

* **2. 内置方法**
```powershell
$BoxSetting.MregeAll()
```

| Container | Type | Parameter | Resume
|-----------|:----:|:---------:|-------
| `MergeAll()`    | public  | No | **重铸所有框架画布设置**
|                 |         |    |
| MergeContent()  | private | No | 重铸内容设置
| MergePadding()  | private | No | 重铸框内空白
| MergeBorder()   | private | No | 重铸框线设置
| MergeMargin()   | private | No | 重铸框外空白
| MergePosition() | private | No | 重铸上述有关的坐标锚点

#### 2. BoxWrite
* **1. 外部传参**
```powershell
$BoxWrite = New-Object SelectBoxDll.BoxWrite
# 分别设置
## NewSet(BoxSetting newset)
$BoxWrite.NewSet($BoxSetting)
$BoxWrite.ReadContentSet = [SelectBoxDll.ReadContentObj]::new($true, $true)

# 合并设置
## NewSet(BoxSetting newset, ReadContentObj readset)
$BoxWrite.NewSet($BoxSetting, [SelectBoxDll.ReadContentObj]::new($true, $true))

# 颜色组设置
## NewColorSet(Colors margin, Colors padding, Colors border, Colors content, Colors highlight, Colors pointer)
$Margin = $Padding = $Content = [SelectBoxDll.Colors]::new($true)
$Border = [SelectBoxDll.Colors]::new([ConsoleColor]::DarkGray, [Console]::BackgroundColor)
$HighLight = [SelectBoxDll.Colors]::new([ConsoleColor]::Yellow, [Console]::BackgroundColor)
$Pointer = [SelectBoxDll.Colors]::new([ConsoleColor]::Yellow, [Console]::BackgroundColor)

$BoxWrite.NewColorSet($Margin, $Padding, $Border, $Content, $HighLight, $Pointer)
```

| Container | Type | Transferable | Resume
|-----------|------|:------------:|-------
| `Set`            | BoxSetting     | Yes | 整体框架设置
| `ColorsSet`      | GraphColorsObj | Yes | 整体颜色设置
| `ReadContentSet` | ReadContentObj | Yes | 读取选项设置
|                  |                |     |
| MarginGraph      | String[]       | No  | 框外空白画布
| PaddingGraph     | String[]       | No  | 框内空白画布
| BorderGraph      | String[]       | No  | 整体框线画布
| ContentGraph     | String[]       | No  | 内容选项画布

* **2. 内置方法**
```powershell
$BoxWrite.WriteBorder()        # 绘制整体画布
$BoxWrite.ReadContent()        # 方向键选定
$BoxWrite.ReadContentSet.Index # 选定索引值
$BoxWrite.ReadContentSet.Value # 选定内容值
```

| Container | Type | Parameter | Resume
|-----------|:----:|:---------:|-------
| `WriteBorder()` | public  | No  | **绘制选项框整体**
| `ReadContent()` | public  | No  | **读取选项内容**
|                 |         |     |
| MergeGraph()    | private | Yes | 重铸画布
| WriteGraph()    | private | Yes | 自上而下绘制画布
| WriteStrXY()    | private | Yes | 按索引在内容锚点输出字符串

#### 3. Function
> 封装的静态方法, 和一些重复较多的简单代码


* 1.分辨中占 2 字节的`字符串实际长度`
```cs
// TODO: 字符串的真实长度
public static Int32 RawLength(String String) { return System.Text.Encoding.Default.GetBytes(String).Length; }
```
* 2.实现`字符串重复`多次后拼接为新的字符串
```cs
// TODO: 字符重复多次
public static String RepeatStr(String str, Int32 num) { return new String('+', num).Replace("+", str); }
```
* 3.获得整数数组中的`最大值`
```cs
// TODO: 整数最大值
public static Int32 IntMax(params Int32[] Int32s) { return Enumerable.Max(Int32s); }
```
* 4.为后续`循环开关`提供跳转索引序列组
```cs
// TODO: 获取序列按指定行列排序后, 四个边界的索引
public static Dictionary<String, Int32[]> GetBorderIndex(Int32 Count, Int32 Column, Boolean ByColumn)
```

* Powershell Example
```powershell
[SelectBoxDll.Function]::RawLength("测试") # 返回值为 4
```

### **Eumn Help**
-----------------

#### 1. AlignmentEnum
* 画布对齐方式, `powershell` 调用方式如下
```powershell
# 作用相同
[SelectBoxDll.AlignmentEnum]::Left
[SelectBoxDll.AlignmentEnum]0
```

| Container | Value | Resume
| ----------|:-----:|-------
| Left      | 0     | 左对齐
| Center    | 1     | 居中
| Right     | 2     | 右对齐

#### 2. PreBorderEnum
* 边框预设模板, `powershell` 调用方式如下
```powershell
# 作用相同
[SelectBoxDll.PreBorderEnum]::AllThinLine
[SelectBoxDll.PreBorderEnum]0
```

| Container | Value | Resume
| ----------|:-----:|-------
| AllThinLine              | 0 | 全细边框
| AllBoldLine              | 1 | 全粗边框
| AllDoubleLine            | 2 | 全双线边框
| ThinBorderRadius         | 3 | 圆角细边框
| BoldVerticalLineOnly     | 4 | 仅粗竖线
| BoldHorizontalLineOnly   | 5 | 仅粗横线
| DoubleVerticalLineOnly   | 6 | 仅竖双线
| DoubleHorizontalLineOnly | 7 | 仅横双线

### **Struct Help**
--------------------

#### 1. Position
* 坐标结构体
```powershell
# 默认初始化为当前光标所在
# [SelectBoxDll.Position]::new(Boolean init)
$Pos = [SelectBoxDll.Position]::new($true)

# 传入坐标 x = 0, y = 0
# [SelectBoxDll.Position]::new(Int32 x, Int32 y)
$Pos = [SelectBoxDll.Position]::new(1, 1)

# powershell 简单修改值
$Pos.Left = 10 # 修改 Left = 10
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `Left`    | Int32 | Yes | 左边界, 即水平坐标
| `Top`     | Int32 | Yes | 顶部边界, 即纵向坐标

#### 2. Colors
* 颜色结构体
```powershell
# 默认初始化为当前前景色与背景色
# [SelectBoxDll.Colors]::new(Boolean init)
$Colors = [SelectBoxDll.Colors]::new($true)

# 传入 fg = [ConsoleColor]::DarkGray, bg = [ConsoleColor]::Yellow
# [SelectBoxDll.Colors]::new(ConsoleColor fg, ConsoleColor bg)
$Colors = [SelectBoxDll.Colors]::new([ConsoleColor]::DarkGray,[ConsoleColor]14)
$Colors = [SelectBoxDll.Colors]::new(1,14)

# powershell 简单修改值
$Colors.ForegroundColor = [ConsoleColor]1 # 设置前景色为深灰色
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `ForegroundColor` | ConsoleColor | Yes | 前景色(字体颜色)
| `BackgroundColor` | ConsoleColor | Yes | 背景色(底纹填充)

#### 3. MarginPaddingObj
* 框外与框内空白结构体
```powershell
# 默认初始化为 -1 或 0
# [SelectBoxDll.MarginPaddingObj]::new(Boolean init)
$Margin = [SelectBoxDll.MarginPaddingObj]::new($true)

# 传入 left = 10, top = 10, 此时左右上下总是对称
# [SelectBoxDll.MarginPaddingObj]::new(Int32 left, Int32 top)
$Margin = [SelectBoxDll.MarginPaddingObj]::new(10, 10)

# 传入 left = 10, top = 10, right = 5, buttom = 6
# [SelectBoxDll.MarginPaddingObj]::new(Int32 left, Int32 top, Int32 right, Int32 buttom)
$Margin = [SelectBoxDll.MarginPaddingObj]::new(10, 10, 5, 6)

# powershell 简单修改值
$Margin.Left = 10 # 设置 Left = 10
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `Top`     | Int32                   | Yes | 顶部空白
| `Buttom`  | Int32                   | Yes | 底部空白
| `Left`    | Int32                   | Yes | 左侧空白
| `Right`   | Int32                   | Yes | 右侧空白
|           |                         |     |
| Width     | Int32                   | No  | 左右宽度
| Height    | Int32                   | No  | 上下高度
| Position  | [Position](#1-position) | No  | 左上角坐标

#### 4. BorderObj
* 框线结构体
```powershell
# 默认初始化为边框全空(null)
# [SelectBoxDll.BorderObj]::new(Boolean init)
$Border = [SelectBoxDll.BorderObj]::new($true)

# 左右边框对称且上下边框对称
# [SelectBoxDll.BorderObj]::new(String left, String top,
#             String lefttop, String righttop, String leftButtom, String rightButtom)
$Border = [SelectBoxDll.BorderObj]::new("│", "━", "┍", "┑", "┕", "┙")

# 全自定义边框
# [SelectBoxDll.BorderObj]::new(String left, String top, String right, String buttom,
#             String lefttop, String righttop, String leftButtom, String rightButtom)
$Border = [SelectBoxDll.BorderObj]::new("│", "━", "│", "━", "┍", "┑", "┕", "┙")

# 使用预设的边框, 搭配 enum 枚举器使用, 见本文 Enum Help
# [SelectBoxDll.BorderObj]::new(PreBorderEnum custom)
$Border = [SelectBoxDll.BorderObj]::new([SelectBoxDll.PreBorderEnum]::AllThinLine)
$Border = [SelectBoxDll.BorderObj]::new([SelectBoxDll.PreBorderEnum]0)

# powershell 简单修改值
$Border.Top = "━"          # 设置 Top = "━"
$Border.Top = [char]0x2501 # 设置 Top = "━"
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `Top`         | String                  | Yes | 顶部边框
| `Buttom`      | String                  | Yes | 底部边框
| `Left`        | String                  | Yes | 左侧边框
| `Right`       | String                  | Yes | 右侧边框
| `LeftTop`     | String                  | Yes | 左上角边框
| `RightTop`    | String                  | Yes | 右上角边框
| `LeftButtom`  | String                  | Yes | 左下角边框
| `RightButtom` | String                  | Yes | 右下角边框
|               |                         |     |
| Width         | Int32                   | No  | 左右宽度
| Height        | Int32                   | No  | 上下高度
| TopMaxLen     | Int32                   | No  | 顶部最大行数
| ButtomMaxLen  | Int32                   | No  | 底部最大行数
| LeftMaxLen    | Int32                   | No  | 左侧最大长度
| RightMaxLen   | Int32                   | No  | 右侧最大长度
| Position      | [Position](#1-position) | No  | 左上角坐标

#### 5. ContentObj
* 内容结构体
```powershell
# 默认初始化为边框全空
# [SelectBoxDll.ContentObj]::new(Boolean init)
$Content = [SelectBoxDll.ContentObj]::new($true)

# 传入 pointer = "> ", strs = [String[]]("One", "Two") 即字符串数组
# [SelectBoxDll.ContentObj]::new(String pointer, params String[] strs)
$Content = [SelectBoxDll.ContentObj]::new("> ", "One", "Two")

# powershell 简单修改值
$Content.Strings = [String[]]("One", "Two") # 设置 Strings = [String[]]("One", "Two")
$Content.Pointer = "> "                     # 设置 Pointer = "> "
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `Strings` | String[]                  | Yes | 字符串数组
| `Pointer` | String                    | Yes | 左侧指示字符串
|           |                           |     |
| Flush     | String[]                  | No  | 空白字符串数组
| Positions | [Position](#1-position)[] | No  | 各字符串坐标数组

#### 6. ContentSetObj
* 内容设置结构体
```powershell
# 默认初始化为 0 列, -1 列间距, 按行排序, 画布居中
# [SelectBoxDll.ContentSetObj]::new(Boolean init)
$ContentSet = [SelectBoxDll.ContentSetObj]::new($true)

# 传入 col = 3, colindent = int[] (1), bycol = false, alignment = AlignmentEnum.Left
# [SelectBoxDll.ContentSetObj]::new(Int32 col, Int32[] colindent, Boolean bycol, AlignmentEnum alignment)
$ContentSet = [SelectBoxDll.ContentSetObj]::new(3, [int[]](1), $false, [SelectBoxDll.AlignmentEnum]::Left)

# powershell 简单修改值
$ContentSet.Column = 3 # 设置 Column = 3
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `Column`    | Int32                             | Yes | 列数
| `ColIndent` | Int32[]                           | Yes | 列间距
| `ByColumn`  | Boolean                           | Yes | 按列排序
| `Alignment` | [AlignmentEnum](#1-alignmentenum) | Yes | 对齐方式
|             |                                   |     |
| Row         | Int32                             | No  | 行数
| Count       | Int32                             | No  | 内容总个数
| Width       | Int32                             | No  | 总宽度
| Height      | Int32                             | No  | 总高度
| ColWidth    | Int32[]                           | No  | 列宽
| RowHeight   | Int32[]                           | No  | 行高
| RowIndent   | Int32[]                           | No  | 行间距
| Position    | [Position](#1-position)           | No  | 左上角坐标

#### 7. GraphColorsObj
* 绘制颜色结构体
```powershell
# 默认初始化为 new Colors(true) 数组, 即当前前景色与背景色
# [SelectBoxDll.GraphColorsObj]::new(Boolean init)
$ColorsSet = [SelectBoxDll.GraphColorsObj]::new($true)

# powershell 简单修改值, 见 Colors 结构体
$ColorsSet.Margin = [SelectBoxDll.Colors]::new([ConsoleColor]::DarkGray,[ConsoleColor]14)
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `Margin`    | [Colors](#2-colors) | Yes | 框外空白
| `Border`    | [Colors](#2-colors) | Yes | 框线搭配
| `Padding`   | [Colors](#2-colors) | Yes | 框内空白
| `Content`   | [Colors](#2-colors) | Yes | 整体内容
| `HighLight` | [Colors](#2-colors) | Yes | 高亮选项
| `Pointer`   | [Colors](#2-colors) | Yes | 着重符号

#### 8. ReadContentObj
* 读取选项结构体
```powershell
# 默认初始化为 方向键, 关闭循环
# [SelectBoxDll.ReadContentObj](Boolean init)
$ReadContentSet = [SelectBoxDll.ReadContentObj]::new($true)

# 设置 键位为方向键, 开启循环
# [SelectBoxDll.ReadContentObj](Boolean init, Boolean recycle)
$ReadContentSet = [SelectBoxDll.ReadContentObj]::new($true, $true)

# 设置 键位为 WASD, 开启循环
# [SelectBoxDll.ReadContentObj](String up, String down, String left, String right, Boolean recycle)
$ReadContentSet = [SelectBoxDll.ReadContentObj]::new("W","S","A","D",$true)

# powershell 简单修改值
$ReadContentSet.UpArrow = "DownArrow" # 向上键为向下键
$ReadContentSet.UpArrow = [ConsoleKey]::DownArrow.ToString() # 同上
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `UpArrow`    | String  | Yes | 向左移动
| `DownArrow`  | String  | Yes | 向下移动
| `LeftArrow`  | String  | Yes | 向上移动
| `RightArrow` | String  | Yes | 向右移动
| `Boolean`    | Recycle | Yes | 循环开关
|              |         |     |
| Index        | Int32   | No  | 选中索引号
| Value        | String  | No  | 选中内容值

* 键位字符串来自枚举器 `[System.ConsoleKey]` 的

