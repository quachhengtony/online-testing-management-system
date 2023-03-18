namespace WebApp.Constants
{
    public static class ErrorConstants
    {
        public static string Success = "Success";
        public static string Failed = "Failed";
        public static string SameBatchDifferentKeyCode = "The test appears to be in the same batch as other test(s). If so, please use the same key code. If not, please create the test using another unique input for the batch field.";
        public static string SomethingWentWrong = "Something went wrong";
        public static string InvalidKeyCode = "The keycode is invalid. Please enter another keycode.";
        public static string TestNotStart = "The test is not start.";
        public static string TestEnded = "The test is already end.";
        public static string InvalidRetakeTest = "You're already done this test.";
        public static string InvalidSubmitTest = "This test is fail because test taker did not submit properly.";
    }
}
