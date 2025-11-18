using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMS.Applications.Subscriptions
{
    public partial class frmSubscriptionBasicInfo : Form
    {
        public frmSubscriptionBasicInfo(int SubscriptionID)
        {
            InitializeComponent();
            ctrlSubscriptionInfo1.LoadSubscriptionInfoByID(SubscriptionID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
