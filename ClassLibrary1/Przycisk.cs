using Hydra;
using System.Windows.Forms;
using System.Drawing;

[assembly: CallbackAssemblyDescription(Name: "Zamykanie Wielu Raportów",
                                           Description: "Pozwala na zakmniecie wielu raportów",
                                           Author: "OneSupport",
                                           Version: "1.0",
                                           SystemVersion: "2019.3.2",
                                           Created: "12-30-2019")]

namespace one017
{
    [SubscribeProcedure(Procedures.KAREdycja, "Zamykanie Wielu Raportów")]
    public class ZamykanieWieluBtn : Callback
    {
        private ClaWindow ACCButton;
        ClaWindow zakladkaRaporty;
        ClaWindow zapiszBtn;

        public override void Init()
        {
            AddSubscription(true, 0, Events.OpenWindow, new TakeEventDelegate(OnOpenWindow));
            AddSubscription(false, 0, Events.ResizeWindow, new TakeEventDelegate(Resize));
        }

        bool OnOpenWindow(Procedures ProcId, int ControlId, Events Event)
        {

            ClaWindow Parent = GetWindow();

            Rectangle WndBnd = GetWindow().Bounds;
            zakladkaRaporty = GetWindow().AllChildren["?Raporty"];
            ACCButton = zakladkaRaporty.Children.Add(ControlTypes.button);
            ACCButton.Visible = true;
            ACCButton.TextRaw = "ACC";
            ACCButton.ToolTipRaw = "Zamykanie wszystkich zaznaczonych raportów.";
            ACCButton.OnAfterMouseDown += new TakeEventDelegate(ACCOnMouseDown);

            zapiszBtn = GetWindow().AllChildren["?Cli_Zapisz"];                            //pobiera współrzędne przycisku zapisz
            ACCButton.Bounds = new Rectangle(zapiszBtn.Bounds.X, zapiszBtn.Bounds.Y + 36, 17, 15);

                          //pobrane z pola w zakładce Raporty 
            return true;
        }
        private bool Resize(Procedures ProcId, int ControlId, Events Event)
        {

            zapiszBtn = GetWindow().AllChildren["?Cli_Zapisz"];
            ACCButton.Bounds = new Rectangle(zapiszBtn.Bounds.X, zapiszBtn.Bounds.Y + 36, 17, 15);

            return true;
        }

        bool ACCOnMouseDown(Procedures ProcedureId, int ControlId, Events Event)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WyborSciezek());



            return true;

        }
        public override void Cleanup()
        {
        }
    }
}