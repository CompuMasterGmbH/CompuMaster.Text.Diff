static class SampleMain
{
    public static void Main()
    {
        const string text1 = "This is a demo\r\nThis is a test of the diff implementation, with some text that is deleted.\r\n\r\n\t- a 1st bullet point\r\n\t- a 2nd bullet point\r\n\r\n\0End of diff demo.";
        const string text2 = "This is a demo\r\nThis is another test of the same implementation, with some more text.\r\n\r\n\t- a 1st bullet point\r\n\t- a 2nd bullet point\r\n\r\nEnd of diff demo.";

        System.Console.WriteLine("==== Diff2Console ====");
        CompuMaster.Text.Diffs.DumpDiffToConsole(text1, text2);
        System.Console.WriteLine();
        System.Console.WriteLine("==== Diff2Console+HumanFriendlyControlChars ====");
        CompuMaster.Text.Diffs.DumpDiffToConsole(text1, text2, true);
        System.Console.WriteLine();
        System.Console.WriteLine("==== Diff2ConsoleShort+HumanFriendlyControlChars ====");
        CompuMaster.Text.Diffs.DumpDiffToConsoleShort(text1, text2, true);
        System.Console.WriteLine();
        System.Console.WriteLine("==== Diff2Html (Input as Text) ====");
        string Text2HtmlOut = CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.TextInputToBeEncodedIntoHtmlBeforeOutput);
        System.Console.WriteLine(Text2HtmlOut);
        System.Console.WriteLine();
        System.Console.WriteLine("==== Diff2Html (Input as Html) ====");
        string Html2HtmlOut = CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.HtmlInputWithoutRequirementOfEncodingBeforeOutput);
        System.Console.WriteLine(Html2HtmlOut);
        System.Console.WriteLine();
        System.Console.WriteLine("==== Diff2Html+HumanFriendlyControlChars (Input as Text) ====");
        string Text2HtmlOutHumanFriendly = CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.TextInputToBeEncodedIntoHtmlBeforeOutput, true);
        System.Console.WriteLine(Text2HtmlOutHumanFriendly);
        System.Console.WriteLine();
        System.Console.WriteLine("==== Diff2Html+HumanFriendlyControlChars (Input as Html) ====");
        string Html2HtmlOutHumanFriendly = CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.HtmlInputWithoutRequirementOfEncodingBeforeOutput, true);
        System.Console.WriteLine(Html2HtmlOutHumanFriendly);
        System.Console.WriteLine();

        string TempHtmlFile = System.IO.Path.GetTempFileName() + ".html";
        System.IO.File.WriteAllText(TempHtmlFile, "<h1>Diff2Html (Input as Text)</h1>" + Text2HtmlOut +
                                             "<h1>Diff2Html (Input as Html)</h1>" + Html2HtmlOut +
                                             "<h1>Diff2Html+HumanFriendlyControlChars (Input as Text)</h1>" + Text2HtmlOutHumanFriendly +
                                             "<h1>Diff2Html+HumanFriendlyControlChars (Input as Html)</h1>" + Html2HtmlOutHumanFriendly);

        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(TempHtmlFile) { UseShellExecute = true });
    }
}