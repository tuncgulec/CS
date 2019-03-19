using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TLDAPComp;

namespace DemoLDAP
{
    public partial class Form1 : Form
    {
        TLDAP LDAP;
        public Form1()
        {
            InitializeComponent();
            LDAP = new TLDAP();
            LDAP.OnLogin += LDAP_OnLogin;
            LDAP.OnGetUsers += LDAP_OnGetUsers;
        }

        private void LDAP_OnGetUsers(object sender, List<TLdapUser> domainUsers, string message)
        {
            if (domainUsers!=null)
            {
                if (domainUsers.Count>0)
                {
                    foreach (var user in domainUsers)
                    {
                        LstUsers.Items.Add("Username: "+user.UserName + " name: " + user.name + " Display: " + user.DisplayName + " desc: " + user.description);
                    }
                }
            }
        }

        private void LDAP_OnLogin(object sender, TDomainUser domainUser, string message)
        {
            if (domainUser!=null)
            {
                LblStatus.Text = "Success";                
                LblDisplayName.Text = domainUser.DisplayName;
                MessageBox.Show(domainUser.GiveName);
            }
            else
                LblStatus.Text = "Error " + message;                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            LDAP.DomainName = TxtDomainAddress.Text;
            LDAP.Login(TxtUsername.Text, TxtPassword.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LDAP.DomainName = TxtDomainAddress.Text;
            LDAP.DomainUserName = TxtUsername.Text;
            LDAP.DomainUserPassword = TxtPassword.Text;
            LstUsers.Items.Clear();
            LDAP.GetUsers();
        }
    }
}
