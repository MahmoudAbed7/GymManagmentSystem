using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMS
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            //ctrlPersonCard1.LoadPersonInfo(15);
            //ctrlEquipmentCard1.LoadEquipmentInfo(13);
            //ctrlApplicationBasicInfo1.LoadApplicationInfo(9);
            //ctrlTrainerCard1.LoadTrainerInfo(2);
            ctrlMembershipCardInfo1.LoadInfo(2);
        }
    }
}
