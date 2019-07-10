using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;

namespace WorkTogether
{
    public class OnSignUpEventArgs : EventArgs
    {
        private string mVorname;
        private string mNachname;
        private string mEMail;
        private string mPasswort;
        private string mPLZ;

        public string Vorname
        {
            get { return mVorname; }
            set { mVorname = value; }
        }

        public string Nachname
        {
            get { return mNachname; }
            set { mNachname = value; }
        }

        public string EMail
        {
            get { return mEMail; }
            set { mEMail = value; }
        }

        public string Passwort
        {
            get { return mPasswort; }
            set { mPasswort = value; }
        }

        public string PLZ
        {
            get { return mPLZ; }
            set { mPLZ = value; }
        }

        public OnSignUpEventArgs(string vorname, string nachname, string email, string passwort, string plz) : base()
        {
            Vorname = vorname;
            Nachname = nachname;
            EMail = email;
            Passwort = passwort;
            PLZ = plz;
        }
    }
    class dialog_SignUp : DialogFragment
    {
        private EditText mTxtVorname;
        private EditText mTxtNachname;
        private EditText mTxtEMail;
        private EditText mTxtPasswort;
        private EditText mTxtPLZ;

        private Button mBtnSignUp;
        private TextView mTxtMistake;

        public event EventHandler<OnSignUpEventArgs> mOnSignUpComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.dialog_sign_up, container, false);


            mTxtVorname = view.FindViewById<EditText>(Resource.Id.txtVorname);
            mTxtNachname = view.FindViewById<EditText>(Resource.Id.txtNachname);
            mTxtEMail = view.FindViewById<EditText>(Resource.Id.txtEMail);
            mTxtPasswort = view.FindViewById<EditText>(Resource.Id.txtPasswort);
            mTxtPLZ = view.FindViewById<EditText>(Resource.Id.txtPLZ);
            mBtnSignUp = view.FindViewById<Button>(Resource.Id.btnDialogEmail);
            mTxtMistake = view.FindViewById<TextView>(Resource.Id.txtMistakeR);

            mBtnSignUp.Click += mBtnSignUp_Click;

            return view;

        }

        void mBtnSignUp_Click(object sender, EventArgs e)
        {
            //User has clicked the sign up button
            if(mTxtVorname.Text == "")
            {
                mTxtMistake.Text = "Bitte geben Sie einen Vornamen ein";
            }
            else
            {
                if(mTxtNachname.Text == "")
                {
                    mTxtMistake.Text = "Bitte geben Sie einen Nachnamen ein";
                }
                else
                {
                    if(mTxtEMail.Text == "")
                    {
                        mTxtMistake.Text = "Bitte geben Sie eine E-Mail ein";
                    }
                    else
                    {
                        if(mTxtPasswort.Text == "")
                        {
                            mTxtMistake.Text = "Bitte geben Sie ein Passwort ein";
                        }
                        else
                        {
                            if(mTxtPLZ.Text == "")
                            {
                                mTxtMistake.Text = "Bitte geben Sie eine PLZ ein";
                            }
                            else
                            {
                                mOnSignUpComplete.Invoke(this, new OnSignUpEventArgs(mTxtVorname.Text, mTxtNachname.Text, mTxtEMail.Text, mTxtPasswort.Text, mTxtPLZ.Text));
                                if (NutzerModel.Mail != null)
                                {
                                    mTxtMistake.Text = "Die E-Mail ist schon vergeben!";
                                }
                                else
                                {
                                    NutzerModel.Vorname = mTxtVorname.Text;
                                    NutzerModel.Nachname = mTxtNachname.Text;
                                    NutzerModel.Mail = mTxtEMail.Text;
                                    NutzerModel.Passwort = mTxtPasswort.Text;
                                    NutzerModel.PLZ = int.Parse(mTxtPLZ.Text);
                                    NutzerModel.Admin = 0;
                                    NutzerModel.ID = 500;
                                    NutzerModel.saveUser();
                                    this.Dismiss();
                                }
                            }
                        }
                    }
                }
            }
            
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }
    }
}