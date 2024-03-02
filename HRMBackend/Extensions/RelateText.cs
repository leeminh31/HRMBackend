using System.Text.RegularExpressions;
using System.Text;

namespace HRMBackend.Extensions
{
    public static class RelateText
    {
        /// <summary>
        /// Chức năng: xoá các kí tự khoảng trắng bị lặp lại (2 kí tự space -> 1 kí tự space)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemoveSpaceCharacter(this string source) =>
            string.IsNullOrEmpty(source) ? string.Empty : Regex.Replace(source.Trim(), @"\s{2,}", " ");

        /// <summary>
        /// Chức năng: xoá kí tự khoảng trắng bị lặp và viết hoa tất cả
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToUpperAndRemoveSpace(this string source) =>
            RemoveSpaceCharacter(source).ToUpper();

        /// <summary>
        /// Chức năng: thay thế kí tự trong một chuỗi cho trước
        /// </summary>
        /// <param name="source"></param>
        /// <param name="old"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static string ReplaceChar(this string source, char old = ':', char @new = '_') =>
            string.IsNullOrEmpty(source) ? string.Empty : source.Replace(old, @new);

        /// <summary>
        /// Chức năng: làm sạch dữ liệu base64 trước khi sử dụng
        /// </summary>
        /// <param name="base64Data"></param>
        /// <returns></returns>
        public static string GetBase64Data(this string base64Data)
        {
            int base64Index = base64Data.IndexOf("base64,");
            string trimData = base64Data.RemoveSpaceCharacter();

            if (base64Index != -1)
                return trimData.Substring(base64Index + 7);
            else
                return trimData;
        }

        /// <summary>
        /// Chức năng: chuyển text từ unicode sang dạng in hoa, viết liền, không dấu
        /// </summary>
        /// <param name="textUnicode"></param>
        /// <returns></returns>
        public static string RemoveSignUnicodeString(this string textUnicode)
        {
            if (!string.IsNullOrEmpty(textUnicode?.Trim()))
            {
                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string temp = textUnicode.Normalize(NormalizationForm.FormD);
                var text = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToUpper();
                return text.Replace(" ", string.Empty);
            }
            else
            {
                return textUnicode;
            }
        }

        /// <summary>
        /// Chức năng : tạo OTP
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateRandomOTP(int length = 6)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public static string ToValidPath(this string path)
        {
            return Path.GetInvalidFileNameChars()
                .Aggregate(path, (previous, current) => previous.Replace(current.ToString(), string.Empty));
        }
    }
}
