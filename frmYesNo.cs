namespace RudnevDragProject
{
    public partial class frmYesNo : MaterialSkin.Controls.MaterialForm
    {
        public frmYesNo()
        {
            InitializeComponent();
            this.CustomMessageBox();
        }
        public string Message
        {
            get { return materialLabel1.Text; }
            set { materialLabel1.Text = value; }
        }
    }
}
