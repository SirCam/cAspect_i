Option Explicit On
Imports System.IO
Imports System.Windows.Forms

Namespace cAspect_i
    Public Class Attributes_i

#Region "Vars"
        Public Shared Obj As Object : Public Shared Typ As Type
        Public Shared Src As String : Public Shared Bse As Form
#End Region

        Public Shared Function returnTest(ByVal i As Object)
            Return i
        End Function

        Public Shared Sub SetBase(ByRef i As Form)
            Bse = i
        End Sub

        Public Shared Sub SetObj(ByRef i As Object)
            If Bse Is Nothing Then
                Throw New Exception("Base has not been initialized. Use SetBase(i), where 'i' is the Parent for each Form Object.")
                Return
            Else
                Obj = i : Typ = Obj.GetType()
            End If
        End Sub

        Public Shared Sub Push(i As String, ii As Object)
            If ii.ToString.Contains(",") = False Then
                CallByName(New cAspect_i.oBase_i,
                       Typ.Name.ToLower.TrimEnd(CType("1234567890-_", Char())) & "_" & i.Replace(".", "_"),
                       CallType.Method,
                       {ii})
            Else
                CallByName(New cAspect_i.oBase_i,
                       Typ.Name.ToLower.TrimEnd(CType("1234567890-_", Char())) & "_" & i.Replace(".", "_"),
                       CallType.Method,
                       {ii})
            End If
        End Sub

        Public Shared Sub Pull(ByVal i As String)
            'Src = i
            'Dim f As String() = File.ReadAllLines(i)
            'If Bse.GetType.Name = Obj.GetType.Name Then
            '    For Each i In f.ToArray
            '        If i.StartsWith("SetObj") Then
            '            Dim Method As String = i.Split(CType("(", Char()))(0) : Dim Obj As Object = Bse
            '            SetObj(Obj)
            '        ElseIf i.StartsWith("Push") Then
            '            Dim Method As String = i.Split(CType("(", Char()))(0) : Dim Args As String = i.Split(CChar("("))(1).Split(CChar("{"))(0).Trim(CChar("(),{} ")) : Dim Args2 As Object() = i.Split(CChar("{"))(1).Split(CChar("}"))(0).Split(CChar(","))
            '            Push(Args, {Args2(0).ToString})
            '        End If
            '    Next
            'Else
            '    If i.StartsWith("SetObj") Then
            '        Dim Method As String = i.Split(CType("(", Char()))(0) : Dim Obj As Object = Bse.Controls.Find(i.Split(CType("(", Char()))(1).Split(CType(")", Char()))(0), True)(0)
            '        SetObj(Obj)
            '    ElseIf i.StartsWith("Push") Then
            '        Dim Method As String = i.Split(CType("(", Char()))(0) : Dim Args() As Object = i.Split(CType("(", Char()))(1).Split(CType(")", Char()))(0).Replace(Method, "").Split(CType(",", Char()))
            '        CallByName(New fBase_i,
            '               Method,
            '               CallType.Method,
            '               Args)
            '    End If
            'End If
            Src = i
            Dim f As String() = File.ReadAllLines(i)
            For Each ii In f.ToArray
                If ii.StartsWith("SetObj(") Then
                    Dim temp = ii.Replace("SetObj(", "").Trim(")")
                    Dim tempobj
                    If temp.ToString = Bse.Name.ToString.ToLower Then
                        tempobj = Bse
                    Else
                        tempobj = Bse.Controls.Find(temp, True)(0)
                    End If
                    SetObj(tempobj)
                ElseIf ii.StartsWith("Push(") Then
                    Dim thempmethod As String = ii.Split("(")(1).Split("{")(0).Split(",")(0).Replace(".", "_") & "_ByString"

                    Dim args As Object = ii.Split("{")(1).Split("}")(0)
                    Push(thempmethod, (args))
                End If
            Next

        End Sub

    End Class

End Namespace
