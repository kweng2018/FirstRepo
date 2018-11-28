Imports System.Xml

Public Class clsXML
    Private XmlDoc As Xml.XmlDocument
    Private XmlFile As String
    Private Const SP As String = "/ "
    Private Const BLK As String = " "

    Public ReadOnly Property XmlFileName() As String
        Get
            Return XmlFile
        End Get
    End Property

    Public ReadOnly Property XmlText() As String
        Get
            Return XmlDoc.InnerXml
        End Get
    End Property

    Sub New(ByVal FileName As String, Optional ByVal CreateNew As Boolean = True, Optional ByVal Root As String = "XML ", Optional ByRef IsOK As Boolean = False)
        IsOK = False
        XmlFile = BLK
        Dim reader As System.Xml.XmlReader = Nothing
        Try
            reader = New System.Xml.XmlTextReader(FileName)
            reader.Read()
        Catch ex As Exception
            If reader IsNot Nothing Then reader.Close()
            Debug.Print("New   -   " & ex.Message)
            If Not Create(FileName, Root) Then Return
        Finally
            If reader IsNot Nothing Then reader.Close()
        End Try

        IsOK = True
        XmlFile = FileName
        XmlDoc = New XmlDocument
        XmlDoc.Load(XmlFile)
    End Sub

    Public Function Create(ByVal FileName As String, Optional ByVal Root As String = "XML ") As Boolean
        Dim NewXML As XmlTextWriter = Nothing
        Try
            NewXML = New XmlTextWriter(FileName, Nothing)
            NewXML.Formatting = Formatting.Indented
            NewXML.WriteStartDocument()
            NewXML.WriteComment(My.Application.Info.AssemblyName & "   Settings ")
            NewXML.WriteStartElement(Root)
            NewXML.WriteAttributeString("Powered ", "Null ")
            NewXML.WriteEndElement()
            NewXML.WriteEndDocument()
            NewXML.Flush()
            NewXML.Close()
        Catch ex As Exception
            Debug.Print("Create   -   " & ex.Message)
            Return False
        Finally
            If NewXML IsNot Nothing Then
                NewXML.Close()
                NewXML = Nothing
            End If
        End Try
        Return True
    End Function

    Public Function Save(ByVal aSection As String, ByVal aKey As String, ByVal aValue As String) As Boolean
        Dim Paths() As String
        Dim n As Integer
        Dim Node, Node2 As XmlNode
        Dim Ele As XmlElement

        While Strings.Left(aSection, 1) = SP
            aSection = Strings.Mid(aSection, 2)
        End While

        '&apos;段名是否为空                 
        If aSection = BLK Then
            XmlDoc.DocumentElement.RemoveAll()
        Else
            Paths = Strings.Split(aSection, SP)
            Try
                Node = XmlDoc.DocumentElement.SelectSingleNode(Paths(n))
                If Node Is Nothing Then
                    Ele = XmlDoc.CreateElement(Paths(n))
                    Node = XmlDoc.DocumentElement.AppendChild(Ele)
                End If
                For n = 1 To Paths.Length - 1
                    If Paths(n) = BLK Then Continue For
                    Node2 = Node.SelectSingleNode(Paths(n))
                    If Node2 Is Nothing Then
                        Ele = XmlDoc.CreateElement(Paths(n))
                        Node2 = Node.AppendChild(Ele)
                    End If
                    Node = Node2
                Next
                '&apos;键名是否为空                                 
                If aKey = BLK Then
                    Node.RemoveAll()
                Else
                    Ele = Node.Item(aKey)
                    If Ele Is Nothing Then
                        Ele = XmlDoc.CreateElement(aKey)
                        Node.AppendChild(Ele)
                    End If
                    '&apos;值是否为空                                         
                    If aValue = BLK Then
                        ' Node.RemoveChild(Ele)
                        Ele.InnerText = aValue
                    Else
                        Ele.InnerText = aValue
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                Return False
            End Try
        End If
        XmlDoc.Save(XmlFile)
        Return True
    End Function

    Public Function Read(ByVal aSection As String, ByVal aKey As String, Optional ByVal aDefaultValue As String = BLK) As String
        Dim Node As XmlNode
        Node = XmlDoc.DocumentElement.SelectSingleNode(aSection & SP & aKey)
        If Node Is Nothing Then Return aDefaultValue
        Return Node.InnerText
    End Function

    Public Function GetSections(ByVal aSection As String, ByRef ListName() As String) As Boolean
        'Node = XmlDoc.DocumentElement.
        Dim Paths() As String
        Dim n As Integer, NodeCount As Integer
        Dim Node, Node2 As XmlNode
        Dim Ele As XmlElement

        While Strings.Left(aSection, 1) = SP
            aSection = Strings.Mid(aSection, 2)
        End While

        '&apos;段名是否为空                 
        If aSection = BLK Then
            Node = XmlDoc.DocumentElement.FirstChild
            NodeCount = XmlDoc.DocumentElement.ChildNodes.Count - 1
            ReDim ListName(NodeCount)
            For n = 0 To NodeCount
                ListName(n) = Node.Name
                Node = Node.NextSibling
            Next
        Else
            Paths = Strings.Split(aSection, SP)
            Try

                Node = XmlDoc.DocumentElement.SelectSingleNode(Paths(n)) 'Node = XmlDoc.DocumentElement.SelectSingleNode(Paths(n))
                If Not Node Is Nothing And Node.HasChildNodes Then

                    For n = 1 To Paths.Length - 1
                        If Paths(n) = BLK Then Continue For
                        Node2 = Node.SelectSingleNode(Paths(n))
                        If Node2 Is Nothing Then
                            Ele = XmlDoc.CreateElement(Paths(n))
                            Node2 = Node.AppendChild(Ele)
                        End If
                        Node = Node2
                    Next

                    NodeCount = Node.ChildNodes.Count - 1
                    Node2 = Node.FirstChild
                    ReDim ListName(NodeCount)
                    For n = 0 To NodeCount
                        ListName(n) = Node2.Name
                        Node2 = Node2.NextSibling
                    Next
                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                Return False
            End Try
        End If
        Return True
    End Function

    Public Function DeleteSection(ByVal aSection As String, ByVal OnlyChildNode As Boolean) As Boolean
        Dim Paths() As String
        Dim n As Integer
        Dim Node As XmlNode

        While Strings.Left(aSection, 1) = SP
            aSection = Strings.Mid(aSection, 2)
        End While

        '&apos;段名是否为空                 
        If aSection = BLK Then
            XmlDoc.DocumentElement.RemoveAll()
        Else
            Paths = Strings.Split(aSection, SP)
            Try
                Node = XmlDoc.DocumentElement.SelectSingleNode(Paths(n))
                If Not Node Is Nothing Then
                    If OnlyChildNode Then
                        Node.RemoveAll()
                    Else
                        XmlDoc.DocumentElement.RemoveChild(Node)
                    End If

                End If
            Catch ex As Exception
                Debug.Print(ex.Message)
                Return False
            End Try
        End If
        XmlDoc.Save(XmlFile)
        Return True
    End Function

End Class
