<%@ Page Title="Departments Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentsDetails.aspx.cs" Inherits="comp2007_week6_lesson6C.DepartmentDetails" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <h1>Departments Details</h1>
                <h5>All Fields are Required</h5>
                <br />
                <div class="form-group">
                    <label class="control-label" for="NameTextBox">Department Name</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="NameTextBox" placeholder="Department Name" required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label" for="BudgetTextBox"><i class="fa fa-usd fa-sm"></i> Budget</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="BudgetTextBox" placeholder="Branch" required="true"></asp:TextBox>
                </div>           
                <div class="text-right">
                    <asp:Button Text="Cancel" ID="CancelButton" CssClass="btn btn-warning btn-lg" runat="server" 
                        UseSubmitBehavior="false" CausesValidation="false"  OnClick="CancelButton_Click"/>
                    <asp:Button Text="Save" ID="SaveButton" CssClass="btn btn-primary btn-lg" runat="server" Onclick="SaveButton_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
