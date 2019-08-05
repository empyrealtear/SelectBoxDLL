Clear-Host
[void][reflection.assembly]::LoadFile("D:\Files\Program\C#\SelectBox\SelectBox.dll")

$BoxSetting = New-Object SelectBoxDll.BoxSetting
$BoxWrite = New-Object SelectBoxDll.BoxWrite
$BoxSetting.NewSet($false, [SelectBoxDll.Position]::new($true))

$Strings = [string[]]("One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten")

$BoxSetting.Content = [SelectBoxDll.ContentObj]::new("> ", $Strings)
$BoxSetting.ContentSet = [SelectBoxDll.ContentSetObj]::new($Strings.Count, [int[]](1), $false, [SelectBoxDll.AlignmentEnum]::Left)
# FIXME: ����������ʱ, ���ܻ���ֲ�ͬ������Ӧͬһ����, ��Щ��������Ч��ֵ
# HACK:  ���������ó�����Чֵʱ, ��ת����������

$BoxSetting.Margin = $BoxSetting.Padding = [SelectBoxDll.MarginPaddingObj]::new(2, 0)
# $BoxSetting.Border = [SelectBoxDll.BorderObj]::new("��", "��", "��", "��", "��", "��")
# $BoxSetting.Border = [SelectBoxDll.BorderObj]::new([char]0x25B6, [char]0x2500, [char]0x256D, [char]0x2570, [char]0x256E, [char]0x256F)
# $BoxSetting.Border = [SelectBoxDll.BorderObj]::new("", "", "", "", "", "") # �ȼ���δ����
# $BoxSetting.Border = [SelectBoxDll.BorderObj]::new("", "", "", "", "", "", "", "") # �±߽�Ϊ��ʱ, ����
# FIXME: ���±߽�Ϊ�ջ���ַ���ʱ, ���±߽籨��
# HACK:  �������±߽�Ļ��Ƴ���ʱ, ʹ�������±߽糤����Ϊ������, ��Ҫ�Ķ��Ƚ�����, ���ҽ��ϱ߽����С���ȸ�Ϊ�� 1

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

