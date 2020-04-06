namespace RudnevDragProject
{
    public partial class frmOK : MaterialSkin.Controls.MaterialForm
    {
        public frmOK()
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
