Option Explicit On
Option Strict On

Imports DiffLib
Imports CompuMaster.VisualBasicCompatibility

Namespace CompuMaster.Text

    Public NotInheritable Class Diffs

#Region "DumpDiff2Console"
        ''' <summary>
        ''' Prepare diff output for colored text console presentation with shortened diff result output with max. 15 chars before and after a difference locations
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        Public Shared Sub DumpDiffToConsoleShort(text1 As String, text2 As String)
            DumpDiffToConsoleShort(text1, text2, 15, False)
        End Sub

        ''' <summary>
        ''' Prepare diff output for colored text console presentation with shortened diff result output with max. 15 chars before and after a difference locations
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="humanFriendlyEncodingOfControlChars">Make control chars CR, LF, TAB, VTAB, FF, BACK, ESC, NUL visible at result</param>
        Public Shared Sub DumpDiffToConsoleShort(text1 As String, text2 As String, humanFriendlyEncodingOfControlChars As Boolean)
            DumpDiffToConsoleShort(text1, text2, 15, humanFriendlyEncodingOfControlChars)
        End Sub

        ''' <summary>
        ''' Prepare diff output for colored text console presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="charsMargin">-1 for full text output, other values shorten the diff result output with max. amount of chars before and after a difference locations</param>
        Public Shared Sub DumpDiffToConsoleShort(text1 As String, text2 As String, charsMargin As Integer)
            If charsMargin < 0 Then
                DumpDiffToConsole(text1, text2, False)
            Else
                DumpDiffToConsoleShort(text1, text2, charsMargin, False)
            End If
        End Sub

        ''' <summary>
        ''' Prepare diff output for colored text console presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="charsMargin">-1 for full text output, other values shorten the diff result output with max. amount of chars before and after a difference locations</param>
        ''' <param name="humanFriendlyEncodingOfControlChars">Make control chars CR, LF, TAB, VTAB, FF, BACK, ESC, NUL visible at result</param>
        Public Shared Sub DumpDiffToConsoleShort(text1 As String, text2 As String, charsMargin As Integer, humanFriendlyEncodingOfControlChars As Boolean)
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            If charsMargin < 0 Then
                DumpDiffToConsole(text1, text2, sections, humanFriendlyEncodingOfControlChars)
            Else
                DumpDiffToConsoleShort(text1, text2, sections, charsMargin, humanFriendlyEncodingOfControlChars)
            End If
        End Sub

        ''' <summary>
        ''' Prepare diff output for colored text console presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        Public Shared Sub DumpDiffToConsole(text1 As String, text2 As String)
            DumpDiffToConsole(text1, text2, False)
        End Sub

        ''' <summary>
        ''' Prepare diff output for colored text console presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="humanFriendlyEncodingOfControlChars">Make control chars CR, LF, TAB, VTAB, FF, BACK, ESC, NUL visible at result</param>
        Public Shared Sub DumpDiffToConsole(text1 As String, text2 As String, humanFriendlyEncodingOfControlChars As Boolean)
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            DumpDiffToConsole(text1, text2, sections, humanFriendlyEncodingOfControlChars)
        End Sub

        ''' <summary>
        ''' Prepare diff output for colored text console presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="sections"></param>
        ''' <param name="humanFriendlyEncodingOfControlChars">Make control chars CR, LF, TAB, VTAB, FF, BACK, ESC, NUL visible at result</param>
        Private Shared Sub DumpDiffToConsole(text1 As String, text2 As String, sections As IEnumerable(Of DiffSection), humanFriendlyEncodingOfControlChars As Boolean)
            Dim DefaultColor As ConsoleColor = Console.ForegroundColor
            Dim DefaultBackgroundColor As ConsoleColor = Console.BackgroundColor
            Dim i1 = 0
            Dim i2 = 0
            For Each section In sections
                If section.IsMatch Then
                    Console.ForegroundColor = DefaultColor
                    Console.BackgroundColor = DefaultBackgroundColor
                    Console.Write(EncodeForConsole(text1.Substring(i1, section.LengthInCollection1), humanFriendlyEncodingOfControlChars))
                Else
                    Console.ForegroundColor = ConsoleColor.White
                    Console.BackgroundColor = ConsoleColor.Red
                    Console.Write(EncodeForConsole(text1.Substring(i1, section.LengthInCollection1), humanFriendlyEncodingOfControlChars))
                    Console.ForegroundColor = ConsoleColor.Green
                    Console.BackgroundColor = DefaultBackgroundColor
                    Console.Write(EncodeForConsole(text2.Substring(i2, section.LengthInCollection2), humanFriendlyEncodingOfControlChars))
                End If

                i1 += section.LengthInCollection1
                i2 += section.LengthInCollection2
            Next
            Console.ForegroundColor = DefaultColor
            Console.BackgroundColor = DefaultBackgroundColor
        End Sub

        ''' <summary>
        ''' Prepare diff output for colored text console presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="sections"></param>
        ''' <param name="charsMargin">0 for full text output, other values shorten the diff result output with max. amount of chars before and after a difference locations</param>
        ''' <param name="humanFriendlyEncodingOfControlChars">Make control chars CR, LF, TAB, VTAB, FF, BACK, ESC, NUL visible at result</param>
        Private Shared Sub DumpDiffToConsoleShort(text1 As String, text2 As String, sections As IEnumerable(Of DiffSection), charsMargin As Integer, humanFriendlyEncodingOfControlChars As Boolean)
            Dim DefaultColor As ConsoleColor = Console.ForegroundColor
            Dim DefaultBackgroundColor As ConsoleColor = Console.BackgroundColor
            Dim i1 = 0
            Dim i2 = 0
            For Each section In sections
                If section.IsMatch Then
                    Console.ForegroundColor = DefaultColor
                    Console.BackgroundColor = DefaultBackgroundColor
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
                    Console.ForegroundColor = ConsoleColor.White
                    Console.BackgroundColor = ConsoleColor.Red
                    Console.Write(EncodeForConsole(text1.Substring(i1, section.LengthInCollection1), humanFriendlyEncodingOfControlChars))
                    Console.ForegroundColor = ConsoleColor.Green
                    Console.BackgroundColor = DefaultBackgroundColor
                    Console.Write(EncodeForConsole(text2.Substring(i2, section.LengthInCollection2), humanFriendlyEncodingOfControlChars))
                End If

                i1 += section.LengthInCollection1
                i2 += section.LengthInCollection2
            Next
            Console.ForegroundColor = DefaultColor
            Console.BackgroundColor = DefaultBackgroundColor
        End Sub
#End Region

#Region "DumpDiff2Text"

        ''' <summary>
        ''' Prepare diff output for colored text console presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        Public Shared Function DumpDiffAsText(text1 As String, text2 As String) As String
            Return DumpDiffAsText(text1, text2, False)
        End Function

        ''' <summary>
        ''' Prepare diff output for colored text console presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="humanFriendlyEncodingOfControlChars">Make control chars CR, LF, TAB, VTAB, FF, BACK, ESC, NUL visible at result</param>
        Public Shared Function DumpDiffAsText(text1 As String, text2 As String, humanFriendlyEncodingOfControlChars As Boolean) As String
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            Return DumpDiffAsText(text1, text2, sections, humanFriendlyEncodingOfControlChars, "{BEGIN REMOVED}", "{/END REMOVED}", "{BEGIN ADDED}", "{/END ADDED}")
        End Function

        ''' <summary>
        ''' Prepare diff output for colored text console presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="humanFriendlyEncodingOfControlChars">Make control chars CR, LF, TAB, VTAB, FF, BACK, ESC, NUL visible at result</param>
        Public Shared Function DumpDiffAsText(text1 As String, text2 As String, humanFriendlyEncodingOfControlChars As Boolean,
                                              textInsertedBeforeRemovedText As String,
                                              textInsertedBehindRemovedText As String,
                                              textInsertedBeforeAddedText As String,
                                              textInsertedBehingAddedText As String
                                              ) As String
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            Return DumpDiffAsText(text1, text2, sections, humanFriendlyEncodingOfControlChars,
                                              textInsertedBeforeRemovedText,
                                              textInsertedBehindRemovedText,
                                              textInsertedBeforeAddedText,
                                              textInsertedBehingAddedText)
        End Function

        ''' <summary>
        ''' Prepare diff output for colored text console presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="sections"></param>
        ''' <param name="humanFriendlyEncodingOfControlChars">Make control chars CR, LF, TAB, VTAB, FF, BACK, ESC, NUL visible at result</param>
        Private Shared Function DumpDiffAsText(text1 As String, text2 As String, sections As IEnumerable(Of DiffSection), humanFriendlyEncodingOfControlChars As Boolean,
                                              textInsertedBeforeRemovedText As String,
                                              textInsertedBehindRemovedText As String,
                                              textInsertedBeforeAddedText As String,
                                              textInsertedBehingAddedText As String
                                              ) As String
            Dim Result = New System.Text.StringBuilder()
            Dim i1 = 0
            Dim i2 = 0
            For Each section In sections
                If section.IsMatch Then
                    Result.Append(EncodeForConsole(text1.Substring(i1, section.LengthInCollection1), humanFriendlyEncodingOfControlChars))
                Else
                    'Removed text
                    If section.LengthInCollection1 > 0 Then
                        Result.Append(textInsertedBeforeRemovedText + EncodeForConsole(text1.Substring(i1, section.LengthInCollection1), humanFriendlyEncodingOfControlChars) + textInsertedBehindRemovedText)
                    End If
                    'Added text
                    If section.LengthInCollection2 > 0 Then
                        Result.Append(textInsertedBeforeAddedText + EncodeForConsole(text2.Substring(i2, section.LengthInCollection2), humanFriendlyEncodingOfControlChars) + textInsertedBehingAddedText)
                    End If
                End If

                i1 += section.LengthInCollection1
                i2 += section.LengthInCollection2
            Next
            Return Result.ToString()
        End Function
#End Region

#Region "DumpDiff2Html"

        ''' <summary>
        ''' Text input encoding information
        ''' </summary>
        Public Enum EncodingRequirement As Byte
            ''' <summary>
            ''' Text input is plain text
            ''' </summary>
            TextInputToBeEncodedIntoHtmlBeforeOutput = 0
            ''' <summary>
            ''' Text input is HTML
            ''' </summary>
            HtmlInputWithoutRequirementOfEncodingBeforeOutput = 1
        End Enum

        ''' <summary>
        ''' Prepare diff output for HTML presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="inputType">Consider input as text or HTML encoded</param>
        ''' <returns>A diff output ready for embedding in HTML</returns>
        Public Shared Function DumpDiffAsHtml(text1 As String, text2 As String, inputType As EncodingRequirement) As String
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            Return DumpDiffAsHtml(text1, text2, inputType, sections, False)
        End Function

        ''' <summary>
        ''' Prepare diff output for HTML presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="inputType">Consider input as text or HTML encoded</param>
        ''' <returns>A diff output ready for embedding in HTML</returns>
        <Obsolete("Use DumpDiffAsHtml with equal result"), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Shared Function DumpDiffAsHtmlHumanFriendly(text1 As String, text2 As String, inputType As EncodingRequirement) As String
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            Return DumpDiffAsHtml(text1, text2, inputType, sections, False)
        End Function

        ''' <summary>
        ''' Prepare diff output for HTML presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="inputType">Consider input as text or HTML encoded</param>
        ''' <param name="humanFriendlyEncodingOfControlChars">Make control chars CR, LF, TAB, VTAB, FF, BACK, ESC, NUL visible at result</param>
        ''' <returns>A diff output ready for embedding in HTML</returns>
        Public Shared Function DumpDiffAsHtml(text1 As String, text2 As String, inputType As EncodingRequirement, humanFriendlyEncodingOfControlChars As Boolean) As String
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            Return DumpDiffAsHtml(text1, text2, inputType, sections, humanFriendlyEncodingOfControlChars)
        End Function

        ''' <summary>
        ''' Prepare diff output for HTML presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="inputType">Consider input as text or HTML encoded</param>
        ''' <param name="humanFriendlyEncodingOfControlChars">Make control chars CR, LF, TAB, VTAB, FF, BACK, ESC, NUL visible at result</param>
        ''' <returns>A diff output ready for embedding in HTML</returns>
        <Obsolete("Use DumpDiffAsHtml with equal result"), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
        Public Shared Function DumpDiffAsHtmlHumanFriendly(text1 As String, text2 As String, inputType As EncodingRequirement, humanFriendlyEncodingOfControlChars As Boolean) As String
            Dim sections As IEnumerable(Of DiffSection) = DiffLib.Diff.CalculateSections(Of Char)(text1.ToCharArray, text2.ToCharArray)
            Return DumpDiffAsHtml(text1, text2, inputType, sections, humanFriendlyEncodingOfControlChars)
        End Function

        ''' <summary>
        ''' Prepare diff output for HTML presentation
        ''' </summary>
        ''' <param name="text1">Initial text</param>
        ''' <param name="text2">Changed text</param>
        ''' <param name="inputType">Consider input as text or HTML encoded</param>
        ''' <param name="sections"></param>
        ''' <param name="humanFriendlyEncodingOfControlChars">Make control chars CR, LF, TAB, VTAB, FF, BACK, ESC, NUL visible at result</param>
        ''' <returns>A diff output ready for embedding in HTML</returns>
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
                Return System.Net.WebUtility.HtmlEncode(text).Replace("&", "&amp;").Replace(" ", "&nbsp;").Replace(ControlChars.CrLf, "<br />").Replace(ControlChars.Cr, "<br />").Replace(ControlChars.Lf, "<br />").Replace(ControlChars.Tab, "&nbsp;&nbsp;&nbsp;&nbsp;")
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