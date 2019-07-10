using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Android.Content;
using Android.Provider;

namespace WorkTogether
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button mBtnSignIn;
        private Button mBtnSignUp;
        private EditText mTxtMail;
        private EditText mTxtPassword;
        private TextView mtxtMistake;
        private RestClient rClient;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            rClient = new RestClient();

            SetContentView(Resource.Layout.login);

            mBtnSignIn = FindViewById<Button>(Resource.Id.btnLogin);
            mBtnSignUp = FindViewById<Button>(Resource.Id.btnDialogSignIn);
            mTxtMail = FindViewById<EditText>(Resource.Id.txtMail);
            mTxtPassword = FindViewById<EditText>(Resource.Id.txtPasswort);
            mtxtMistake = FindViewById<TextView>(Resource.Id.txtMistake);
            
            mBtnSignIn.Click += mBtnSignIn_Click;
            mBtnSignUp.Click += mBtnSignUp_Click;
        }

        private void mBtnSignUp_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialog_SignUp signUpDialog = new dialog_SignUp();
            signUpDialog.Show(transaction, "dialog fragment");

            signUpDialog.mOnSignUpComplete += signUpDialog_mOnSignUpComplete;
        }

        private void signUpDialog_mOnSignUpComplete(object sender, OnSignUpEventArgs e)
        {
            rClient.endPoint = "http://h2793624.stratoserver.net/php_rest/api/post/read_user_single.php?Mail=" + e.EMail;
            string response = string.Empty;
            response = rClient.makeRequest();
            NutzerModel.readSingleUser(response);
        }

        private void mBtnSignIn_Click(object sender, EventArgs e)
        {
            if (mTxtMail.Text == "")
            {
                mtxtMistake.Text = "Geben Sie Ihre E-Mail ein";
            }
            else
            {
                if (mTxtPassword.Text == "")
                {
                    mtxtMistake.Text = "Geben Sie Ihr Passwort ein";
                }
                else
                {
                    rClient.endPoint = "http://h2793624.stratoserver.net/php_rest/api/post/read_user_single.php?Mail=" + mTxtMail.Text;
                    string response = string.Empty;
                    response = rClient.makeRequest();
                    NutzerModel.readSingleUser(response);
                    if (mTxtPassword.Text.Equals(NutzerModel.Passwort))
                    {
                        Console.WriteLine("Erfolgreich");
                        //Intent contacts = new Intent(this, typeof(Contacts));
                        //StartActivity(contacts);
                    }
                    else
                    {
                        mtxtMistake.Text = "E-Mail oder Passwort falsch";
                    }

                }
            }
        }

    }
}

