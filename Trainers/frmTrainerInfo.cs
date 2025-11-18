using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMS.Trainers
{
    public partial class frmTrainerInfo : Form
    {
        public frmTrainerInfo(int TrainerID)
        {
            InitializeComponent();
            ctrlTrainerCard1.LoadTrainerInfo(TrainerID);
        }
    }
}
