Option Explicit On
Option Strict On

Imports DiffLib

Namespace CompuMaster.Text

    Public Class Diffs

#Region "DumpDiff2Console"
        Public Shared Sub DumpDiffToConsoleShort(text1 As String, text2 As String)
            DumpDiffToConsole(text1, text2, 15, False)
        End Sub

        Public Shared Sub DumpDiffToConsoleShort(text1 As String, text2 As String, humanFriendlyEncodingOfControlChars As Boolean)
            DumpDiffToConsole(text1, text2, 15, humanFriendlyEncodingOfControlChars)
        End Sub

        Public Shared Sub DumpDiffToConsole(text1 As String, text2 As String, charsMargin As Integer)
            DumpDiffToConsole(text1, text2, charsMargin, False)
        End Sub
        Public Shared Sub DumpDiffToConsole(text1 As String, text2 As String, charsMargin As Integer, humanFriendlyEncodingOfControlChars As Boolean)
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            DumpDiffToConsole(text1, text2, sections, charsMargin, humanFriendlyEncodingOfControlChars)
        End Sub

        Public Shared Sub DumpDiffToConsole(text1 As String, text2 As String)
            DumpDiffToConsole(text1, text2, False)
        End Sub

        Public Shared Sub DumpDiffToConsole(text1 As String, text2 As String, humanFriendlyEncodingOfControlChars As Boolean)
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            DumpDiffToConsole(text1, text2, sections, humanFriendlyEncodingOfControlChars)
        End Sub

        Private Shared Sub DumpDiffToConsole(text1 As String, text2 As String, sections As IEnumerable(Of DiffSection), humanFriendlyEncodingOfControlChars As Boolean)
            Dim DefaultColor As ConsoleColor = Console.ForegroundColor
            Dim html = New System.Text.StringBuilder()
            Dim i1 = 0
            Dim i2 = 0
            For Each section In sections
                If section.IsMatch Then
                    Console.ForegroundColor = DefaultColor
                    Console.Write(EncodeForConsole(text1.Substring(i1, section.LengthInCollection1), humanFriendlyEncodingOfControlChars))
                Else
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.Write(EncodeForConsole(text1.Substring(i1, section.LengthInCollection1), humanFriendlyEncodingOfControlChars))
                    Console.ForegroundColor = ConsoleColor.Green
                    Console.Write(EncodeForConsole(text2.Substring(i2, section.LengthInCollection2), humanFriendlyEncodingOfControlChars))
                End If

                i1 += section.LengthInCollection1
                i2 += section.LengthInCollection2
            Next
            Console.ForegroundColor = DefaultColor
        End Sub

        Private Shared Sub DumpDiffToConsole(text1 As String, text2 As String, sections As IEnumerable(Of DiffSection), charsMargin As Integer, humanFriendlyEncodingOfControlChars As Boolean)
            Dim DefaultColor As ConsoleColor = Console.ForegroundColor
            Dim html = New System.Text.StringBuilder()
            Dim i1 = 0
            Dim i2 = 0
            For Each section In sections
                If section.IsMatch Then
                    Console.ForegroundColor = DefaultColor
                    Dim UnchangedText As String = text1.Substring(i1, section.LengthInCollection1)
                    If i1 = 0 Then
                        'begin of string -> only the char amount of {charsMargin} at the end
                        If UnchangedText.Length <= charsMargin Then
                            Console.Write(EncodeForConsole(UnchangedText, humanFriendlyEncodingOfControlChars))
                        Else
                            Console.Write(EncodeForConsole("..." & UnchangedText.Substring(UnchangedText.Length - charsMargin), humanFriendlyEncodingOfControlChars))
                        End If
                    ElseIf i1 + UnchangedText.Length = text1.Length Then
                        'begin of string -> only the char amount of {charsMargin} at the end
                        If UnchangedText.Length <= charsMargin Then
                            Console.Write(EncodeForConsole(UnchangedText, humanFriendlyEncodingOfControlChars))
                        Else
                            Console.Write(EncodeForConsole(UnchangedText.Substring(0, charsMargin) & "...", humanFriendlyEncodingOfControlChars))
                        End If
                    Else
                        'inside the string -> the char amount of {charsMargin} from the begin and from the end
                        If section.LengthInCollection1 > 2 * charsMargin Then
                            Console.Write(EncodeForConsole(UnchangedText.Substring(0, System.Math.Min(UnchangedText.Length, charsMargin)), humanFriendlyEncodingOfControlChars))
                            Console.Write("...")
                            Console.Write(EncodeForConsole(UnchangedText.Substring(UnchangedText.Length - System.Math.Min(UnchangedText.Length, charsMargin)), humanFriendlyEncodingOfControlChars))
                        Else
                            Console.Write(EncodeForConsole(UnchangedText, humanFriendlyEncodingOfControlChars))
                        End If
                    End If
                Else
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.Write(EncodeForConsole(text1.Substring(i1, section.LengthInCollection1), humanFriendlyEncodingOfControlChars))
                    Console.ForegroundColor = ConsoleColor.Green
                    Console.Write(EncodeForConsole(text2.Substring(i2, section.LengthInCollection2), humanFriendlyEncodingOfControlChars))
                End If

                i1 += section.LengthInCollection1
                i2 += section.LengthInCollection2
            Next
            Console.ForegroundColor = DefaultColor
        End Sub
#End Region

#Region "DumpDiff2Html"

        Public Enum EncodingRequirement As Byte
            TextInputToBeEncodedIntoHtmlBeforeOutput = 0
            HtmlInputWithoutRequirementOfEncodingBeforeOutput = 1
        End Enum

        Public Shared Function DumpDiffAsHtml(text1 As String, text2 As String, inputType As EncodingRequirement) As String
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            Return DumpDiffAsHtml(text1, text2, inputType, sections, False)
        End Function

        Public Shared Function DumpDiffAsHtmlHumanFriendly(text1 As String, text2 As String, inputType As EncodingRequirement) As String
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            Return DumpDiffAsHtml(text1, text2, inputType, sections, False)
        End Function

        Public Shared Function DumpDiffAsHtml(text1 As String, text2 As String, inputType As EncodingRequirement, humanFriendlyEncodingOfControlChars As Boolean) As String
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            Return DumpDiffAsHtml(text1, text2, inputType, sections, humanFriendlyEncodingOfControlChars)
        End Function

        Public Shared Function DumpDiffAsHtmlHumanFriendly(text1 As String, text2 As String, inputType As EncodingRequirement, humanFriendlyEncodingOfControlChars As Boolean) As String
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            Return DumpDiffAsHtml(text1, text2, inputType, sections, humanFriendlyEncodingOfControlChars)
        End Function
        Private Shared Function DumpDiffAsHtml(text1 As String, text2 As String, inputType As EncodingRequirement, sections As IEnumerable(Of DiffSection), humanFriendlyEncodingOfControlChars As Boolean) As String
            Dim html = New System.Text.StringBuilder()
            Dim i1 = 0
            Dim i2 = 0
            For Each section In sections
                If section.IsMatch Then
                    html.Append(EncodeForHtml(EncodeTextForHtml(text1.Substring(i1, section.LengthInCollection1), inputType), humanFriendlyEncodingOfControlChars))
                Else
                    html.Append("<span style=""background-color: #ffcccc; text-decoration: line-through;"">" + EncodeForHtml(EncodeTextForHtml(text1.Substring(i1, section.LengthInCollection1), inputType), humanFriendlyEncodingOfControlChars) + "</span>")
                    html.Append("<span style=""background-color: #ccffcc;"">" + EncodeForHtml(EncodeTextForHtml(text2.Substring(i2, section.LengthInCollection2), inputType), humanFriendlyEncodingOfControlChars) + "</span>")
                End If

                i1 += section.LengthInCollection1
                i2 += section.LengthInCollection2
            Next
            Return html.ToString()
        End Function
#End Region

#Region "Encoding support"
        Private Shared Function EncodeTextForHtml(text As String, inputType As EncodingRequirement) As String
            If inputType = EncodingRequirement.TextInputToBeEncodedIntoHtmlBeforeOutput Then
                Return System.Net.WebUtility.HtmlEncode(text).Replace("&", "&amp;").Replace(" ", "&nbsp;").Replace(vbNewLine, "<br />").Replace(ControlChars.Cr, "<br />").Replace(ControlChars.Lf, "<br />").Replace(ControlChars.Tab, "&nbsp;&nbsp;&nbsp;&nbsp;")
            Else
                Return System.Net.WebUtility.HtmlEncode(text)
            End If
        End Function

        Private Shared Function EncodeForHtml(text As String, humanFriendlyEncodingOfControlChars As Boolean) As String
            If humanFriendlyEncodingOfControlChars Then
                Return text.Replace(ControlChars.Cr, "{CR}").Replace(ControlChars.Lf, "{LF}").Replace(ControlChars.Tab, "{TAB}").Replace(ControlChars.VerticalTab, "{VTAB}").Replace(ControlChars.NullChar, "{NUL}").Replace(ControlChars.Back, "{BACK}").Replace(ControlChars.FormFeed, "{FF}").Replace(ChrW(27), "{ESC}")
            Else
                Return text
            End If
        End Function

        Private Shared Function EncodeForConsole(text As String, humanFriendlyEncodingOfControlChars As Boolean) As String
            If humanFriendlyEncodingOfControlChars Then
                Return text.Replace(ControlChars.Cr, "{CR}").Replace(ControlChars.Lf, "{LF}").Replace(ControlChars.Tab, "{TAB}").Replace(ControlChars.VerticalTab, "{VTAB}").Replace(ControlChars.NullChar, "{NUL}").Replace(ControlChars.Back, "{BACK}").Replace(ControlChars.FormFeed, "{FF}").Replace(ChrW(27), "{ESC}")
                'Return text.Replace(ControlChars.Cr, "<CR>").Replace(ControlChars.Lf, "<LF>").Replace(ControlChars.Tab, "<TAB>").Replace(ControlChars.VerticalTab, "<VTAB>").Replace(ControlChars.NullChar, "<NUL>").Replace(ControlChars.Back, "<BACK>").Replace(ControlChars.FormFeed, "<FF>").Replace(ChrW(27), "<ESC>")
            Else
                Return text
            End If
        End Function
#End Region

    End Class

End Namespace