Imports CommandLine
Imports System

Module MainApp

    Sub Main(ByVal args As String())
        Try
            CommandLine.Parser.Default.ParseArguments(Of Options)(args) _
                .WithParsed(Function(opts As Options) RunOptionsAndReturnExitCode(opts)) _
                .WithNotParsed(Function(errs As IEnumerable(Of [Error])) 1)
        Catch ex As Exception
            System.Console.WriteLine(ex.Message)
            System.Environment.Exit(2)
        End Try
    End Sub

    Function RunOptionsAndReturnExitCode(options As Options) As Integer
        Dim text1 As String = System.IO.File.ReadAllText(options.File1)
        Dim text2 As String = System.IO.File.ReadAllText(options.File2)

        Select Case options.Mode
            Case Options.InputOutputModes.PlainText
                CompuMaster.Text.Diffs.DumpDiffToConsole(text1, text2, options.HumanFriendlyControlChars)
            Case Options.InputOutputModes.PlainTextComparisonWithHtmlOutput
                Dim Text2HtmlOut As String
                Text2HtmlOut = CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.TextInputToBeEncodedIntoHtmlBeforeOutput, options.HumanFriendlyControlChars)
                System.Console.WriteLine(Text2HtmlOut)
            Case Options.InputOutputModes.Html
                Dim Html2HtmlOut As String
                Html2HtmlOut = CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.HtmlInputWithoutRequirementOfEncodingBeforeOutput, options.HumanFriendlyControlChars)
                System.Console.WriteLine(Html2HtmlOut)
            Case Else
                Throw New NotSupportedException("Invalid mode argument")
        End Select
        Return 0
    End Function

End Module
