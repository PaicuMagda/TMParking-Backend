namespace TMParking_Backend.Helpers
{
    public class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken,string nameUser)
        {
            return $@"
        <h1 style='color: #643094;'>Hi, {nameUser}!</h1>
        <h2>A request has been received to change the password for your TMParking account.</h2>
        <a href='http://localhost:4200/reset-password?email={email}&code={emailToken}'>
            <button style='background-color: #643094; color: #fff; padding: 10px 20px; border: none; border-radius: 4px; cursor: pointer;'>
                Reset Password
            </button>
        </a>
         <p> If you did not request a password reset, you can safely ignore this email.</p> 
         <p> Only a person with access to your email can reset your account password.</p>
         <p>Thank you,</p>
         <p>The TMParking Team</p>
        ";
        }
    }
}
