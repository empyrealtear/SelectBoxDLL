## **[SelectBox.dll] Help**
> ����Ϊ `SelectBox.dll` �� `powershell` �е�ʹ�÷���, ����û����ȫ��⵽ `powershell������ģ��` ���������������, ���� `C#` �������εķ�ʽ, ���п��ٽ��������� dll �����С�

## **Content**
--------------

<!-- vim-markdown-toc GFM -->
- [**�෽�� Class Help**](#class-help)
  - [1. ������� BoxSetting](#1-boxsetting)
  - [2. ��ܻ��� BoxWrite  ](#2-boxwrite)
  - [3. ��̬���� Function  ](#3-function)
- [**ö���� Eumn Help**](#eumn-help)
  - [1. ����ö�� AlignmentEnum](#1-alignmentenum)
  - [2. ����ö�� PreBorderEnum](#2-preborderenum)
- [**�ṹ�� Struct Help**](#struct-help)
  - [1. ����ê�� Position](#1-position)
  - [2. ��ɫ��� Colors](#2-colors)
  - [3. �հ����� MarginPaddingObj](#3-marginpaddingobj)
  - [4. �������� BorderObj](#4-borderobj)
  - [5. �����ַ� ContentObj](#5-contentobj)
  - [6. �������� ContentSetObj](#6-contentsetobj)
  - [7. ������ɫ GraphColorsObj](#7-graphcolorsobj)
  - [8. ѡ���ȡ ReadContentObj](#8-readcontentobj)
<!-- vim-markdown-toc -->

--------------

### **FIXME**
```powershell
$BoxSetting.ContentSet = [SelectBoxDll.ContentSetObj]::new($Strings.Count, [int[]](1), $true, [SelectBoxDll.AlignmentEnum]::Left)
# FIXME: ����������ʱ, ���ܻ���ֲ�ͬ������Ӧͬһ����, ��Щ��������Ч��ֵ
# HACK:  ���������ó�����Чֵʱ, ��ת����������

$BoxSetting.Border = [SelectBoxDll.BorderObj]::new("", "", "", "", "", "") # �ȼ���δ����
$BoxSetting.Border = [SelectBoxDll.BorderObj]::new("", "", "", "", "", "", "", "") # �±߽�Ϊ��ʱ, ����
# FIXME: ���±߽�Ϊ�ջ���ַ���ʱ, ���±߽籨��
# HACK:  �������±߽�Ļ��Ƴ���ʱ, ʹ�������±߽糤����Ϊ������, ��Ҫ�Ķ��Ƚ�����, ���ҽ��ϱ߽����С���ȸ�Ϊ�� 1
```

### **Class Help**
------------------

#### 1. BoxSetting
* **1. �ⲿ����**
```powershell
$BoxSetting = New-Object SelectBoxDll.BoxSetting
# public void NewSet(Boolean oldconsole, Position pos)
$BoxSetting.NewSet($false, [SelectBoxDll.Position]::new($true))

# public void NewSet(Boolean oldconsole, Position pos,
#                    MarginPaddingObj marginobj, MarginPaddingObj paddingobj,
#                    BorderObj borderobj, ContentObj contentobj, ContentSetObj contentsetobj)
## �ֲ�����
$BoxSetting.Content = [SelectBoxDll.ContentObj]::new("> ", $Strings)

## ȫ������
$Margin     = [SelectBoxDll.MarginPaddingObj]::new(2, 1)
$Padding    = [SelectBoxDll.MarginPaddingObj]::new(2, 0)
$Border     = [SelectBoxDll.BorderObj]::new([SelectBoxDll.PreBorderEnum]::AllThinLine)
$Content    = [SelectBoxDll.ContentObj]::new("> ",$Strings)
$ContentSet = $ContentSet = [SelectBoxDll.ContentSetObj]::new(3, [int[]](1), $false, [SelectBoxDll.AlignmentEnum]::Left)

$BoxSetting.NewSet($false, [SelectBoxDll.Position]::new($true), $Margin, $Padding, $Border, $Content, $ContentSet)
```

| Container | Type | Transferable | Resume
|-----------|------|:------------:|-------
| `Margin`     | MarginPaddingObj | Yes | ����հ�
| `Border`     | BorderObj        | Yes | �������
| `Padding`    | MarginPaddingObj | Yes | ���ڿհ�
| `Content`    | ContentObj       | Yes | �����ַ�
| `ContentSet` | ContentSetObj    | Yes | ��������
| `Position`   | Position         | Yes | ����ê��
| `OldConsole` | Boolean          | Yes | �ɿ���̨ģʽ

* **2. ���÷���**
```powershell
$BoxSetting.MregeAll()
```

| Container | Type | Parameter | Resume
|-----------|:----:|:---------:|-------
| `MergeAll()`    | public  | No | **�������п�ܻ�������**
|                 |         |    |
| MergeContent()  | private | No | ������������
| MergePadding()  | private | No | �������ڿհ�
| MergeBorder()   | private | No | ������������
| MergeMargin()   | private | No | ��������հ�
| MergePosition() | private | No | ���������йص�����ê��

#### 2. BoxWrite
* **1. �ⲿ����**
```powershell
$BoxWrite = New-Object SelectBoxDll.BoxWrite
# �ֱ�����
## NewSet(BoxSetting newset)
$BoxWrite.NewSet($BoxSetting)
$BoxWrite.ReadContentSet = [SelectBoxDll.ReadContentObj]::new($true, $true)

# �ϲ�����
## NewSet(BoxSetting newset, ReadContentObj readset)
$BoxWrite.NewSet($BoxSetting, [SelectBoxDll.ReadContentObj]::new($true, $true))

# ��ɫ������
## NewColorSet(Colors margin, Colors padding, Colors border, Colors content, Colors highlight, Colors pointer)
$Margin = $Padding = $Content = [SelectBoxDll.Colors]::new($true)
$Border = [SelectBoxDll.Colors]::new([ConsoleColor]::DarkGray, [Console]::BackgroundColor)
$HighLight = [SelectBoxDll.Colors]::new([ConsoleColor]::Yellow, [Console]::BackgroundColor)
$Pointer = [SelectBoxDll.Colors]::new([ConsoleColor]::Yellow, [Console]::BackgroundColor)

$BoxWrite.NewColorSet($Margin, $Padding, $Border, $Content, $HighLight, $Pointer)
```

| Container | Type | Transferable | Resume
|-----------|------|:------------:|-------
| `Set`            | BoxSetting     | Yes | ����������
| `ColorsSet`      | GraphColorsObj | Yes | ������ɫ����
| `ReadContentSet` | ReadContentObj | Yes | ��ȡѡ������
|                  |                |     |
| MarginGraph      | String[]       | No  | ����հ׻���
| PaddingGraph     | String[]       | No  | ���ڿհ׻���
| BorderGraph      | String[]       | No  | ������߻���
| ContentGraph     | String[]       | No  | ����ѡ���

* **2. ���÷���**
```powershell
$BoxWrite.WriteBorder()        # �������廭��
$BoxWrite.ReadContent()        # �����ѡ��
$BoxWrite.ReadContentSet.Index # ѡ������ֵ
$BoxWrite.ReadContentSet.Value # ѡ������ֵ
```

| Container | Type | Parameter | Resume
|-----------|:----:|:---------:|-------
| `WriteBorder()` | public  | No  | **����ѡ�������**
| `ReadContent()` | public  | No  | **��ȡѡ������**
|                 |         |     |
| MergeGraph()    | private | Yes | ��������
| WriteGraph()    | private | Yes | ���϶��»��ƻ���
| WriteStrXY()    | private | Yes | ������������ê������ַ���

#### 3. Function
> ��װ�ľ�̬����, ��һЩ�ظ��϶�ļ򵥴���


* 1.�ֱ���ռ 2 �ֽڵ�`�ַ���ʵ�ʳ���`
```cs
// TODO: �ַ�������ʵ����
public static Int32 RawLength(String String) { return System.Text.Encoding.Default.GetBytes(String).Length; }
```
* 2.ʵ��`�ַ����ظ�`��κ�ƴ��Ϊ�µ��ַ���
```cs
// TODO: �ַ��ظ����
public static String RepeatStr(String str, Int32 num) { return new String('+', num).Replace("+", str); }
```
* 3.������������е�`���ֵ`
```cs
// TODO: �������ֵ
public static Int32 IntMax(params Int32[] Int32s) { return Enumerable.Max(Int32s); }
```
* 4.Ϊ����`ѭ������`�ṩ��ת����������
```cs
// TODO: ��ȡ���а�ָ�����������, �ĸ��߽������
public static Dictionary<String, Int32[]> GetBorderIndex(Int32 Count, Int32 Column, Boolean ByColumn)
```

* Powershell Example
```powershell
[SelectBoxDll.Function]::RawLength("����") # ����ֵΪ 4
```

### **Eumn Help**
-----------------

#### 1. AlignmentEnum
* �������뷽ʽ, `powershell` ���÷�ʽ����
```powershell
# ������ͬ
[SelectBoxDll.AlignmentEnum]::Left
[SelectBoxDll.AlignmentEnum]0
```

| Container | Value | Resume
| ----------|:-----:|-------
| Left      | 0     | �����
| Center    | 1     | ����
| Right     | 2     | �Ҷ���

#### 2. PreBorderEnum
* �߿�Ԥ��ģ��, `powershell` ���÷�ʽ����
```powershell
# ������ͬ
[SelectBoxDll.PreBorderEnum]::AllThinLine
[SelectBoxDll.PreBorderEnum]0
```

| Container | Value | Resume
| ----------|:-----:|-------
| AllThinLine              | 0 | ȫϸ�߿�
| AllBoldLine              | 1 | ȫ�ֱ߿�
| AllDoubleLine            | 2 | ȫ˫�߱߿�
| ThinBorderRadius         | 3 | Բ��ϸ�߿�
| BoldVerticalLineOnly     | 4 | ��������
| BoldHorizontalLineOnly   | 5 | ���ֺ���
| DoubleVerticalLineOnly   | 6 | ����˫��
| DoubleHorizontalLineOnly | 7 | ����˫��

### **Struct Help**
--------------------

#### 1. Position
* ����ṹ��
```powershell
# Ĭ�ϳ�ʼ��Ϊ��ǰ�������
# [SelectBoxDll.Position]::new(Boolean init)
$Pos = [SelectBoxDll.Position]::new($true)

# �������� x = 0, y = 0
# [SelectBoxDll.Position]::new(Int32 x, Int32 y)
$Pos = [SelectBoxDll.Position]::new(1, 1)

# powershell ���޸�ֵ
$Pos.Left = 10 # �޸� Left = 10
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `Left`    | Int32 | Yes | ��߽�, ��ˮƽ����
| `Top`     | Int32 | Yes | �����߽�, ����������

#### 2. Colors
* ��ɫ�ṹ��
```powershell
# Ĭ�ϳ�ʼ��Ϊ��ǰǰ��ɫ�뱳��ɫ
# [SelectBoxDll.Colors]::new(Boolean init)
$Colors = [SelectBoxDll.Colors]::new($true)

# ���� fg = [ConsoleColor]::DarkGray, bg = [ConsoleColor]::Yellow
# [SelectBoxDll.Colors]::new(ConsoleColor fg, ConsoleColor bg)
$Colors = [SelectBoxDll.Colors]::new([ConsoleColor]::DarkGray,[ConsoleColor]14)
$Colors = [SelectBoxDll.Colors]::new(1,14)

# powershell ���޸�ֵ
$Colors.ForegroundColor = [ConsoleColor]1 # ����ǰ��ɫΪ���ɫ
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `ForegroundColor` | ConsoleColor | Yes | ǰ��ɫ(������ɫ)
| `BackgroundColor` | ConsoleColor | Yes | ����ɫ(�������)

#### 3. MarginPaddingObj
* ��������ڿհ׽ṹ��
```powershell
# Ĭ�ϳ�ʼ��Ϊ -1 �� 0
# [SelectBoxDll.MarginPaddingObj]::new(Boolean init)
$Margin = [SelectBoxDll.MarginPaddingObj]::new($true)

# ���� left = 10, top = 10, ��ʱ�����������ǶԳ�
# [SelectBoxDll.MarginPaddingObj]::new(Int32 left, Int32 top)
$Margin = [SelectBoxDll.MarginPaddingObj]::new(10, 10)

# ���� left = 10, top = 10, right = 5, buttom = 6
# [SelectBoxDll.MarginPaddingObj]::new(Int32 left, Int32 top, Int32 right, Int32 buttom)
$Margin = [SelectBoxDll.MarginPaddingObj]::new(10, 10, 5, 6)

# powershell ���޸�ֵ
$Margin.Left = 10 # ���� Left = 10
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `Top`     | Int32                   | Yes | �����հ�
| `Buttom`  | Int32                   | Yes | �ײ��հ�
| `Left`    | Int32                   | Yes | ���հ�
| `Right`   | Int32                   | Yes | �Ҳ�հ�
|           |                         |     |
| Width     | Int32                   | No  | ���ҿ��
| Height    | Int32                   | No  | ���¸߶�
| Position  | [Position](#1-position) | No  | ���Ͻ�����

#### 4. BorderObj
* ���߽ṹ��
```powershell
# Ĭ�ϳ�ʼ��Ϊ�߿�ȫ��(null)
# [SelectBoxDll.BorderObj]::new(Boolean init)
$Border = [SelectBoxDll.BorderObj]::new($true)

# ���ұ߿�Գ������±߿�Գ�
# [SelectBoxDll.BorderObj]::new(String left, String top,
#             String lefttop, String righttop, String leftButtom, String rightButtom)
$Border = [SelectBoxDll.BorderObj]::new("��", "��", "��", "��", "��", "��")

# ȫ�Զ���߿�
# [SelectBoxDll.BorderObj]::new(String left, String top, String right, String buttom,
#             String lefttop, String righttop, String leftButtom, String rightButtom)
$Border = [SelectBoxDll.BorderObj]::new("��", "��", "��", "��", "��", "��", "��", "��")

# ʹ��Ԥ��ı߿�, ���� enum ö����ʹ��, ������ Enum Help
# [SelectBoxDll.BorderObj]::new(PreBorderEnum custom)
$Border = [SelectBoxDll.BorderObj]::new([SelectBoxDll.PreBorderEnum]::AllThinLine)
$Border = [SelectBoxDll.BorderObj]::new([SelectBoxDll.PreBorderEnum]0)

# powershell ���޸�ֵ
$Border.Top = "��"          # ���� Top = "��"
$Border.Top = [char]0x2501 # ���� Top = "��"
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `Top`         | String                  | Yes | �����߿�
| `Buttom`      | String                  | Yes | �ײ��߿�
| `Left`        | String                  | Yes | ���߿�
| `Right`       | String                  | Yes | �Ҳ�߿�
| `LeftTop`     | String                  | Yes | ���ϽǱ߿�
| `RightTop`    | String                  | Yes | ���ϽǱ߿�
| `LeftButtom`  | String                  | Yes | ���½Ǳ߿�
| `RightButtom` | String                  | Yes | ���½Ǳ߿�
|               |                         |     |
| Width         | Int32                   | No  | ���ҿ��
| Height        | Int32                   | No  | ���¸߶�
| TopMaxLen     | Int32                   | No  | �����������
| ButtomMaxLen  | Int32                   | No  | �ײ��������
| LeftMaxLen    | Int32                   | No  | �����󳤶�
| RightMaxLen   | Int32                   | No  | �Ҳ���󳤶�
| Position      | [Position](#1-position) | No  | ���Ͻ�����

#### 5. ContentObj
* ���ݽṹ��
```powershell
# Ĭ�ϳ�ʼ��Ϊ�߿�ȫ��
# [SelectBoxDll.ContentObj]::new(Boolean init)
$Content = [SelectBoxDll.ContentObj]::new($true)

# ���� pointer = "> ", strs = [String[]]("One", "Two") ���ַ�������
# [SelectBoxDll.ContentObj]::new(String pointer, params String[] strs)
$Content = [SelectBoxDll.ContentObj]::new("> ", "One", "Two")

# powershell ���޸�ֵ
$Content.Strings = [String[]]("One", "Two") # ���� Strings = [String[]]("One", "Two")
$Content.Pointer = "> "                     # ���� Pointer = "> "
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `Strings` | String[]                  | Yes | �ַ�������
| `Pointer` | String                    | Yes | ���ָʾ�ַ���
|           |                           |     |
| Flush     | String[]                  | No  | �հ��ַ�������
| Positions | [Position](#1-position)[] | No  | ���ַ�����������

#### 6. ContentSetObj
* �������ýṹ��
```powershell
# Ĭ�ϳ�ʼ��Ϊ 0 ��, -1 �м��, ��������, ��������
# [SelectBoxDll.ContentSetObj]::new(Boolean init)
$ContentSet = [SelectBoxDll.ContentSetObj]::new($true)

# ���� col = 3, colindent = int[] (1), bycol = false, alignment = AlignmentEnum.Left
# [SelectBoxDll.ContentSetObj]::new(Int32 col, Int32[] colindent, Boolean bycol, AlignmentEnum alignment)
$ContentSet = [SelectBoxDll.ContentSetObj]::new(3, [int[]](1), $false, [SelectBoxDll.AlignmentEnum]::Left)

# powershell ���޸�ֵ
$ContentSet.Column = 3 # ���� Column = 3
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `Column`    | Int32                             | Yes | ����
| `ColIndent` | Int32[]                           | Yes | �м��
| `ByColumn`  | Boolean                           | Yes | ��������
| `Alignment` | [AlignmentEnum](#1-alignmentenum) | Yes | ���뷽ʽ
|             |                                   |     |
| Row         | Int32                             | No  | ����
| Count       | Int32                             | No  | �����ܸ���
| Width       | Int32                             | No  | �ܿ��
| Height      | Int32                             | No  | �ܸ߶�
| ColWidth    | Int32[]                           | No  | �п�
| RowHeight   | Int32[]                           | No  | �и�
| RowIndent   | Int32[]                           | No  | �м��
| Position    | [Position](#1-position)           | No  | ���Ͻ�����

#### 7. GraphColorsObj
* ������ɫ�ṹ��
```powershell
# Ĭ�ϳ�ʼ��Ϊ new Colors(true) ����, ����ǰǰ��ɫ�뱳��ɫ
# [SelectBoxDll.GraphColorsObj]::new(Boolean init)
$ColorsSet = [SelectBoxDll.GraphColorsObj]::new($true)

# powershell ���޸�ֵ, �� Colors �ṹ��
$ColorsSet.Margin = [SelectBoxDll.Colors]::new([ConsoleColor]::DarkGray,[ConsoleColor]14)
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `Margin`    | [Colors](#2-colors) | Yes | ����հ�
| `Border`    | [Colors](#2-colors) | Yes | ���ߴ���
| `Padding`   | [Colors](#2-colors) | Yes | ���ڿհ�
| `Content`   | [Colors](#2-colors) | Yes | ��������
| `HighLight` | [Colors](#2-colors) | Yes | ����ѡ��
| `Pointer`   | [Colors](#2-colors) | Yes | ���ط���

#### 8. ReadContentObj
* ��ȡѡ��ṹ��
```powershell
# Ĭ�ϳ�ʼ��Ϊ �����, �ر�ѭ��
# [SelectBoxDll.ReadContentObj](Boolean init)
$ReadContentSet = [SelectBoxDll.ReadContentObj]::new($true)

# ���� ��λΪ�����, ����ѭ��
# [SelectBoxDll.ReadContentObj](Boolean init, Boolean recycle)
$ReadContentSet = [SelectBoxDll.ReadContentObj]::new($true, $true)

# ���� ��λΪ WASD, ����ѭ��
# [SelectBoxDll.ReadContentObj](String up, String down, String left, String right, Boolean recycle)
$ReadContentSet = [SelectBoxDll.ReadContentObj]::new("W","S","A","D",$true)

# powershell ���޸�ֵ
$ReadContentSet.UpArrow = "DownArrow" # ���ϼ�Ϊ���¼�
$ReadContentSet.UpArrow = [ConsoleKey]::DownArrow.ToString() # ͬ��
```

| Container | Type | Transferable | Resume 
| ----------|------|:------------:|--------
| `UpArrow`    | String  | Yes | �����ƶ�
| `DownArrow`  | String  | Yes | �����ƶ�
| `LeftArrow`  | String  | Yes | �����ƶ�
| `RightArrow` | String  | Yes | �����ƶ�
| `Boolean`    | Recycle | Yes | ѭ������
|              |         |     |
| Index        | Int32   | No  | ѡ��������
| Value        | String  | No  | ѡ������ֵ

* ��λ�ַ�������ö���� `[System.ConsoleKey]` ��

