// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("rP56XrpMAlZcvMDnsmUKqfX9j4SSIKOAkq+kq4gk6iRVr6Ojo6eioSCjraKSIKOooCCjo6Id1ivEDq+MbDzX4DluvWJHCDuNCLW6mt/Hjfj8W/NGeSlCxWTwhx7j4T68FyqcQH6fGRgrWkfegx35o1SBrh9NpVEy85IpT9eQq2fxMGw8PEgprB552TPk1tt67KDreM/+VQiUiLkOlOqPNdXPEtVYMsgwEtosYiaepCVKp/DqLrrYW1qP5dZbMOSGy1qffeN0NFTzdl/8ePgQqpwnJ8LBxXn6Uy76dVwnPR0Jzpx297ZDgAyc1tm1piUxJzdT5ygF8gNLTwl4p3pRlW14tdyIytdpNig6sMnJPd22DhEet4V49n+5n8qIaTHR1aCho6Kj");
        private static int[] order = new int[] { 10,10,10,3,11,13,9,11,12,10,11,13,13,13,14 };
        private static int key = 162;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
