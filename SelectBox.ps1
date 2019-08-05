Clear-Host
[void][reflection.assembly]::LoadFile("D:\Files\Program\C#\SelectBox\SelectBox.dll")

$BoxSetting = New-Object SelectBoxDll.BoxSetting
$BoxWrite = New-Object SelectBoxDll.BoxWrite
$BoxSetting.NewSet($false, [SelectBoxDll.Position]::new($true))

$Strings = [string[]]("One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten")

$BoxSetting.Content = [SelectBoxDll.ContentObj]::new("> ", $Strings)
$BoxSetting.ContentSet = [SelectBoxDll.ContentSetObj]::new($Strings.Count, [int[]](1), $false, [SelectBoxDll.AlignmentEnum]::Left)
# FIXME: 当按列排序时, 可能会出现不同列数对应同一行数, 这些列数是无效的值
# HACK:  当列数设置出于无效值时, 可转而设置行数

$BoxSetting.Margin = $BoxSetting.Padding = [SelectBoxDll.MarginPaddingObj]::new(2, 0)
# $BoxSetting.Border = [SelectBoxDll.BorderObj]::new("│", "━", "┍", "┕", "┑", "┙")
# $BoxSetting.Border = [SelectBoxDll.BorderObj]::new([char]0x25B6, [char]0x2500, [char]0x256D, [char]0x2570, [char]0x256E, [char]0x256F)
# $BoxSetting.Border = [SelectBoxDll.BorderObj]::new("", "", "", "", "", "") # 等价于未设置
# $BoxSetting.Border = [SelectBoxDll.BorderObj]::new("", "", "", "", "", "", "", "") # 下边界为空时, 报错
# FIXME: 上下边界为空或空字符串时, 上下边界报错
# HACK:  计算上下边界的绘制长度时, 使用了上下边界长度作为被除数, 若要改动比较困难, 暂且将上边界的最小长度改为了 1

$BoxSetting.Border = [SelectBoxDll.BorderObj]::new([SelectBoxDll.PreBorderEnum]0);

$BoxSetting.MergeAll();


$BoxWrite.NewSet($BoxSetting, [SelectBoxDll.ReadContentObj]::new($true, $true))
# $Margin = $Padding = $Content = [SelectBoxDll.Colors]::new($true)
$Border = [SelectBoxDll.Colors]::new([ConsoleColor]::DarkGray, [Console]::BackgroundColor)
$HighLight = [SelectBoxDll.Colors]::new([ConsoleColor]::Yellow, [Console]::BackgroundColor)
$Pointer = [SelectBoxDll.Colors]::new([ConsoleColor]::Yellow, [Console]::BackgroundColor)
# $BoxWrite.NewColorSet($Margin, $Padding, $Border, $Content, $HighLight, $Pointer)
$defcolors = [SelectBoxDll.Colors]::new($true)
$BoxWrite.NewColorSet($defcolors, $defcolors, $Border, $defcolors, $HighLight, $Pointer)


$BoxWrite.WriteBorder()


# ((Get-Date) - $start).TotalSeconds
$BoxWrite.ReadContent()
# $BoxWrite.ReadContentSet.Index
# $BoxWrite.ReadContentSet.Value
# exit

