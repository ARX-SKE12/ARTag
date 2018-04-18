
namespace ARTag
{
    using UnityEngine;

    public class TextProcessor : MonoBehaviour
    {

        public static string ConvertFromNewLine(string text)
        {
            return text.Replace("\n", "#$");
        }
        public static string ConvertToNewLine(string text)
        {
            return text.Replace("#$", "\n");
        }
    }

}
