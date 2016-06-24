<%@ Page Title="Departments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="comp2007_week6_lesson6C.Departments" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Departments List</h1>
                <a href="/Contoso/DepartmentsDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i> Add Department</a>
                <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover"
                    ID="DepartmentsGridView" AutoGenerateColumns="false" DataKeyNames="DepartmentID" OnRowDeleting="DepartmentsGridView_RowDeleting"
                    AllowSorting="true" OnSorting="DepartmentsGridView_Sorting" OnRowDataBound="DepartmentsGridView_RowDataBound">
                    <Columns>
                        <asp:BoundField  DataField="DepartmentID" HeaderText="Department ID" Visible="true" SortExpression="DepartmentID"/>
                        <asp:BoundField  DataField="Name" HeaderText="Department Name" Visible="true" SortExpression="Name"/>
                        <asp:BoundField  DataField="Budget" HeaderText="Budget" Visible="true" SortExpression="Budget" DataFormatString="{0:C}" />
                        <asp:HyperLinkField HeaderText="Edit"  Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" NavigateUrl="~/Contoso/DepartmentsDetails.aspx.cs"
                            DataNavigateUrlFields="DepartmentID" DataNavigateUrlFormatString="DepartmentsDetails.aspx?DepartmentID={0}"
                            controlStyle-CssClass="btn btn-primary btn-sm"/>
                        <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i>Delete" ShowDeleteButton="true" 
                            ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
