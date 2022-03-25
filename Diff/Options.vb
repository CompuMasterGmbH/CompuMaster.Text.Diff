Imports CommandLine

Public Class Options

    <CommandLine.Value(0, Required:=True,
        HelpText:="Path of file 1")>
    Public Property File1 As String

    <CommandLine.Value(1, Required:=True,
        HelpText:="Path of file 2")>
    Public Property File2 As String

    <[Option]("v", "UseHumanFriendlyControlChars", [Default]:=False,
        HelpText:="Replace control chars by human-readable substitutions")>
    Public Property HumanFriendlyControlChars As Boolean

    Public Enum InputOutputModes As Integer
        PlainText = 0
        PlainTextComparisonWithHtmlOutput = 1
        Html = 2
    End Enum

    <[Option]("m", "Mode", [Default]:=InputOutputModes.PlainText,
              HelpText:="One of the following modes: PlainText, PlainTextComparisonWithHtmlOutput, Html")>
    Public Property Mode As InputOutputModes

End Class
