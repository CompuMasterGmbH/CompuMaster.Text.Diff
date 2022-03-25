Option Explicit On
Option Strict Off

Imports NUnit.Framework

Namespace CompuMaster.Test.Text.Diff

    <TestFixture()> Public Class DiffTest

        Const text1 As String = "This is a demo" & ControlChars.CrLf & "This is a test of the diff implementation, with some text that is deleted." & ControlChars.CrLf & ControlChars.CrLf & vbTab & "- a 1st bullet point" & ControlChars.CrLf & vbTab & "- a 2nd bullet point" & ControlChars.CrLf & ControlChars.CrLf & vbTab & CompuMaster.VisualBasicCompatibility.ControlChars.NullChar & "End of diff demo."
        Const text2 As String = "This is a demo" & ControlChars.CrLf & "This is another test of the same implementation, with some more text." & ControlChars.CrLf & ControlChars.CrLf & vbTab & "- a 1st bullet point" & ControlChars.CrLf & vbTab & "- a 2nd bullet point" & ControlChars.CrLf & ControlChars.CrLf & "End of diff demo."

        <Test> Public Sub DumpDiffAsText()
            Dim ExpectedValue As String

            System.Console.WriteLine(CompuMaster.Text.Diffs.DumpDiffAsText(text1, text2))
            ExpectedValue = "This is a demo" & ControlChars.CrLf &
                "This is a{BEGIN ADDED}nother{/END ADDED} test of the {BEGIN REMOVED}diff{/END REMOVED}{BEGIN ADDED}same{/END ADDED} implementation, with some {BEGIN ADDED}more {/END ADDED}text{BEGIN REMOVED} that is deleted{/END REMOVED}." & ControlChars.CrLf &
                "" & ControlChars.CrLf &
                ControlChars.Tab & "- a 1st bullet point" & ControlChars.CrLf &
                ControlChars.Tab & "- a 2nd bullet point" & ControlChars.CrLf &
                "" & ControlChars.CrLf &
                "{BEGIN REMOVED}" & ControlChars.Tab & ChrW(0) & "{/END REMOVED}End of diff demo."
            Assert.AreEqual(ExpectedValue, CompuMaster.Text.Diffs.DumpDiffAsText(text1, text2))

            ExpectedValue = "This is a demo{CR}{LF}" &
                "This is a{BEGIN ADDED}nother{/END ADDED} test of the {BEGIN REMOVED}diff{/END REMOVED}{BEGIN ADDED}same{/END ADDED} implementation, with some {BEGIN ADDED}more {/END ADDED}text{BEGIN REMOVED} that is deleted{/END REMOVED}.{CR}{LF}" &
                "{CR}{LF}" &
                "{TAB}- a 1st bullet point{CR}{LF}" &
                "{TAB}- a 2nd bullet point{CR}{LF}" &
                "{CR}{LF}" &
                "{BEGIN REMOVED}{TAB}{NUL}{/END REMOVED}End of diff demo."
            System.Console.WriteLine(CompuMaster.Text.Diffs.DumpDiffAsText(text1, text2, True))
            Assert.AreEqual(ExpectedValue, CompuMaster.Text.Diffs.DumpDiffAsText(text1, text2, True))
        End Sub

        <Test> Public Sub DumpDiffAsHtml()
            Dim ExpectedValue As String

            System.Console.WriteLine(CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.TextInputToBeEncodedIntoHtmlBeforeOutput))
            ExpectedValue = "This&nbsp;is&nbsp;a&nbsp;demo<br />This&nbsp;is&nbsp;a<span style=""background-color: #ffcccc; text-decoration: line-through;""></span><span style=""background-color: #ccffcc;"">nother</span>&nbsp;test&nbsp;of&nbsp;the&nbsp;<span style=""background-color: #ffcccc; text-decoration: line-through;"">diff</span><span style=""background-color: #ccffcc;"">same</span>&nbsp;implementation,&nbsp;with&nbsp;some&nbsp;<span style=""background-color: #ffcccc; text-decoration: line-through;""></span><span style=""background-color: #ccffcc;"">more&nbsp;</span>text<span style=""background-color: #ffcccc; text-decoration: line-through;"">&nbsp;that&nbsp;is&nbsp;deleted</span><span style=""background-color: #ccffcc;""></span>.<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;a&nbsp;1st&nbsp;bullet&nbsp;point<br />&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;a&nbsp;2nd&nbsp;bullet&nbsp;point<br /><br /><span style=""background-color: #ffcccc; text-decoration: line-through;"">&nbsp;&nbsp;&nbsp;&nbsp;" & ChrW(0) & "</span><span style=""background-color: #ccffcc;""></span>End&nbsp;of&nbsp;diff&nbsp;demo."
            Assert.AreEqual(ExpectedValue, CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.TextInputToBeEncodedIntoHtmlBeforeOutput))

            System.Console.WriteLine(CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.TextInputToBeEncodedIntoHtmlBeforeOutput, True))
            ExpectedValue = "This&nbsp;is&nbsp;a&nbsp;demo<br />This&nbsp;is&nbsp;a<span style=""background-color: #ffcccc; text-decoration: line-through;""></span><span style=""background-color: #ccffcc;"">nother</span>&nbsp;test&nbsp;of&nbsp;the&nbsp;<span style=""background-color: #ffcccc; text-decoration: line-through;"">diff</span><span style=""background-color: #ccffcc;"">same</span>&nbsp;implementation,&nbsp;with&nbsp;some&nbsp;<span style=""background-color: #ffcccc; text-decoration: line-through;""></span><span style=""background-color: #ccffcc;"">more&nbsp;</span>text<span style=""background-color: #ffcccc; text-decoration: line-through;"">&nbsp;that&nbsp;is&nbsp;deleted</span><span style=""background-color: #ccffcc;""></span>.<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;a&nbsp;1st&nbsp;bullet&nbsp;point<br />&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;a&nbsp;2nd&nbsp;bullet&nbsp;point<br /><br /><span style=""background-color: #ffcccc; text-decoration: line-through;"">&nbsp;&nbsp;&nbsp;&nbsp;{NUL}</span><span style=""background-color: #ccffcc;""></span>End&nbsp;of&nbsp;diff&nbsp;demo."
            Assert.AreEqual(ExpectedValue, CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.TextInputToBeEncodedIntoHtmlBeforeOutput, True))

            System.Console.WriteLine(CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.HtmlInputWithoutRequirementOfEncodingBeforeOutput))
            ExpectedValue = "This is a demo" & ControlChars.CrLf &
                "This is a<span style=""background-color: #ffcccc; text-decoration: line-through;""></span><span style=""background-color: #ccffcc;"">nother</span> test of the <span style=""background-color: #ffcccc; text-decoration: line-through;"">diff</span><span style=""background-color: #ccffcc;"">same</span> implementation, with some <span style=""background-color: #ffcccc; text-decoration: line-through;""></span><span style=""background-color: #ccffcc;"">more </span>text<span style=""background-color: #ffcccc; text-decoration: line-through;""> that is deleted</span><span style=""background-color: #ccffcc;""></span>." & ControlChars.CrLf &
                "" & ControlChars.CrLf &
                ControlChars.Tab & "- a 1st bullet point" & ControlChars.CrLf &
                ControlChars.Tab & "- a 2nd bullet point" & ControlChars.CrLf &
                "" & ControlChars.CrLf &
                "<span style=""background-color: #ffcccc; text-decoration: line-through;"">" & ControlChars.Tab & ChrW(0) & "</span><span style=""background-color: #ccffcc;""></span>End of diff demo."
            Assert.AreEqual(ExpectedValue, CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.HtmlInputWithoutRequirementOfEncodingBeforeOutput))

            System.Console.WriteLine(CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.HtmlInputWithoutRequirementOfEncodingBeforeOutput, True))
            ExpectedValue = "This is a demo{CR}{LF}" &
                "This is a<span style=""background-color: #ffcccc; text-decoration: line-through;""></span><span style=""background-color: #ccffcc;"">nother</span> test of the <span style=""background-color: #ffcccc; text-decoration: line-through;"">diff</span><span style=""background-color: #ccffcc;"">same</span> implementation, with some <span style=""background-color: #ffcccc; text-decoration: line-through;""></span><span style=""background-color: #ccffcc;"">more </span>text<span style=""background-color: #ffcccc; text-decoration: line-through;""> that is deleted</span><span style=""background-color: #ccffcc;""></span>.{CR}{LF}" &
                "{CR}{LF}" &
                "{TAB}- a 1st bullet point{CR}{LF}" &
                "{TAB}- a 2nd bullet point{CR}{LF}" &
                "{CR}{LF}" &
                "<span style=""background-color: #ffcccc; text-decoration: line-through;"">{TAB}{NUL}</span><span style=""background-color: #ccffcc;""></span>End of diff demo."
            Assert.AreEqual(ExpectedValue, CompuMaster.Text.Diffs.DumpDiffAsHtml(text1, text2, CompuMaster.Text.Diffs.EncodingRequirement.HtmlInputWithoutRequirementOfEncodingBeforeOutput, True))
        End Sub

    End Class

End Namespace
