Option Explicit On
Option Strict On

Imports Microsoft.VisualBasic

Module SampleMain

    Sub Main()
        Const text1 As String = "This is a demo" & ControlChars.CrLf & "This is a test of the diff implementation, with some text that is deleted." & ControlChars.CrLf & ControlChars.CrLf & vbTab & "- a 1st bullet point" & ControlChars.CrLf & vbTab & "- a 2nd bullet point" & ControlChars.CrLf & ControlChars.CrLf & vbTab & CompuMaster.VisualBasicCompatibility.ControlChars.NullChar & "End of diff demo."
        Const text2 As String = "This is a demo" & ControlChars.CrLf & "This is another test of the same implementation, with some more text." & ControlChars.CrLf & ControlChars.CrLf & vbTab & "- a 1st bullet point" & ControlChars.CrLf & vbTab & "- a 2nd bullet point" & ControlChars.CrLf & ControlChars.CrLf & "End of diff demo."

        'Console sample output
        Console.WriteLine("===== Diff2Console Plain Text =====")
        Console.WriteLine()
        Console.WriteLine("==== Diff2Console ====")
        CompuMaster.Text.Diffs.DumpDiffToConsole(text1, text2)
        Console.WriteLine()
        Console.WriteLine("==== Diff2Console+HumanFriendlyControlChars ====")
        CompuMaster.Text.Diffs.DumpDiffToConsole(text1, text2, True)
        Console.WriteLine()
        Console.WriteLine("==== Diff2ConsoleShort+HumanFriendlyControlChars ====")
        CompuMaster.Text.Diffs.DumpDiffToConsoleShort(text1, text2, True)

        'Console sample output with plain text output
        Console.WriteLine()
        Console.WriteLine("===== Diff2Console Plain text output =====")
        Console.WriteLine()
        Console.WriteLine("==== DumpDiffAsText ====")
        Dim DumpDiffAsText As String = CompuMaster.Text.Diffs.DumpDiffAsText(text1, text2)
        Console.WriteLine(DumpDiffAsText)
        Console.WriteLine()
        Console.WriteLine("==== DumpDiffAsText+HumanFriendlyControlChars ====")
        Dim DumpDiffAsTextHumanFriendly As String = CompuMaster.Text.Diffs.DumpDiffAsText(text1, text2, True)
        Console.WriteLine(DumpDiffAsTextHumanFriendly)


        'Console sample output with HTML output
        Console.WriteLine()
        Console.WriteLine("===== Diff2Console HTML output =====")
        Console.WriteLine()
        Console.WriteLine("==== Diff2Html (Input as Text) ====")
        Dim Text2HtmlOut As String = CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.TextInputToBeEncodedIntoHtmlBeforeOutput)
        Console.WriteLine(Text2HtmlOut)
        Console.WriteLine()
        Console.WriteLine("==== Diff2Html (Input as Html) ====")
        Dim Html2HtmlOut As String = CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.HtmlInputWithoutRequirementOfEncodingBeforeOutput)
        Console.WriteLine(Html2HtmlOut)
        Console.WriteLine()
        Console.WriteLine("==== Diff2Html+HumanFriendlyControlChars (Input as Text) ====")
        Dim Text2HtmlOutHumanFriendly As String = CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.TextInputToBeEncodedIntoHtmlBeforeOutput, True)
        Console.WriteLine(Text2HtmlOutHumanFriendly)
        Console.WriteLine()
        Console.WriteLine("==== Diff2Html+HumanFriendlyControlChars (Input as Html) ====")
        Dim Html2HtmlOutHumanFriendly As String = CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.HtmlInputWithoutRequirementOfEncodingBeforeOutput, True)
        Console.WriteLine(Html2HtmlOutHumanFriendly)
        Console.WriteLine()

        'HTML output rendered as HTML page in browser
        Dim TempHtmlFile As String = System.IO.Path.GetTempFileName & ".htm"
        System.IO.File.WriteAllText(TempHtmlFile, "<h1>Diff sample page</h1><h2>Diff2Html (Input as Text)</h2>" & Text2HtmlOut & "<h2>Diff2Html (Input as Html)</h2>" & Html2HtmlOut & "<h2>Diff2Html+HumanFriendlyControlChars (Input as Text)</h2>" & Text2HtmlOutHumanFriendly & "<h2>Diff2Html+HumanFriendlyControlChars (Input as Html)</h2>" & Html2HtmlOutHumanFriendly)
        System.Diagnostics.Process.Start(TempHtmlFile)

        'Plain text output
        Dim TempPlainTextFile As String = System.IO.Path.GetTempFileName & ".txt"
        System.IO.File.WriteAllText(TempPlainTextFile,
                                    "# Diff plain text sample" & System.Environment.NewLine &
                                    System.Environment.NewLine &
                                    "## DumpDiffAsText" & System.Environment.NewLine &
                                    DumpDiffAsText & System.Environment.NewLine &
                                    "## DumpDiffAsTextHumanFriendly" & System.Environment.NewLine &
                                    DumpDiffAsTextHumanFriendly
                                    )
        System.Diagnostics.Process.Start(TempPlainTextFile)
    End Sub

End Module
