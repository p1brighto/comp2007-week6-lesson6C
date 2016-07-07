﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements that are required to connect to EF DB
using comp2007_week6_lesson6C.Models;
using System.Web.ModelBinding;

// required for Identity and OWIN Security
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace comp2007_week6_lesson6C.Admin
{
    public partial class UserDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    PasswordPlaceHolder.Visible = false;
                    this.GetUser();
                }
                else
                {
                    PasswordPlaceHolder.Visible = true;

                }
            }
        }
        protected void GetUser()
        {
            string UserID = Request.QueryString["Id"].ToString();

            using (UserConnection db = new UserConnection())
            {
                AspNetUser updatedUser = (from users in db.AspNetUsers
                                          where users.Id == UserID
                                          select users).FirstOrDefault();
                if (updatedUser != null)
                {
                    UserNameTextBox.Text = updatedUser.UserName;
                    PhoneNumberTextBox.Text = updatedUser.PhoneNumber;
                    EmailTextBox.Text = updatedUser.Email;
                }


            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            //redirect to Users Page
            Response.Redirect("~/Admin/Users.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string UserID = "";

            //if updating user
            if (Request.QueryString.Count > 0)
            {
                using (UserConnection db = new UserConnection())
                {
                    AspNetUser newUser = new AspNetUser();

                    UserID = Request.QueryString["Id"].ToString();

                    newUser = (from users in db.AspNetUsers
                               where users.Id == UserID
                               select users).FirstOrDefault();

                    newUser.UserName = UserNameTextBox.Text;
                    newUser.PhoneNumber = PhoneNumberTextBox.Text;
                    newUser.Email = EmailTextBox.Text;

                    db.SaveChanges();

                    //redirect to Users Page
                    Response.Redirect("~/Admin/Users.aspx");

                }
            }
            //if creating new user
            if (UserID == "")
            {
                //create new userSore and user Manager objects
                var userStore = new UserStore<IdentityUser>();
                var userManager = new UserManager<IdentityUser>(userStore);

                // create a new user object
                var user = new IdentityUser()
                {
                    UserName = UserNameTextBox.Text,
                    PhoneNumber = PhoneNumberTextBox.Text,
                    Email = EmailTextBox.Text
                };

                // create a new user in the db and store the result 
                IdentityResult result = userManager.Create(user, PasswordTextBox.Text);

                if (result.Succeeded)
                {
                    Response.Redirect("~/Admin/Users.aspx");
                }
                else
                {
                    StatusLabel.Text = result.Errors.FirstOrDefault();
                    AlertFlash.Visible = true;
                }
            }
        }
    }
}