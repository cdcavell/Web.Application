namespace ClassLibrary.Mvc.Html
{
    public static class Tags
    {
        public static string LineBreak()
        {
            return LineBreak(1);
        }

        public static string LineBreak(int count)
        {
            string value = string.Empty;

            for (int i = 0; i < count; i++)
            {
                value += "<br/>";
            }

            return value;
        }

        public static string Space()
        {
            return Space(1);
        }

        public static string Space(int count)
        {
            string value = string.Empty;

            for (int i = 0; i < count; i++)
            {
                value += "&nbsp;";
            }

            return value;
        }

        public static string Brackets(string item)
        {
            return "[ " + item + " ]";
        }

        public static string Bold(string item)
        {
            return "<b>" + item + "</b>";
        }

        public static string Italic(string item)
        {
            return "<i>" + item + "</i>";
        }

        public static string Strong(string item)
        {
            return "<strong>" + item + "</strong>";
        }

        public static string Emphasized(string item)
        {
            return "<em>" + item + "</em>";
        }

        public static string Marked(string item)
        {
            return "<mark>" + item + "</mark>";
        }

        public static string Small(string item)
        {
            return "<small>" + item + "</small>";
        }

        public static string Deleted(string item)
        {
            return "<del>" + item + "</del>";
        }

        public static string Inserted(string item)
        {
            return "<ins>" + item + "</ins>";
        }

        public static string Subscript(string item)
        {
            return "<sub>" + item + "</sub>";
        }

        public static string Superscript(string item)
        {
            return "<sup>" + item + "</sup>";
        }

        public static string Quote(string item)
        {
            return "<q>" + item + "</q>";
        }

        public static string BlockQuote(string item, string cite)
        {
            return "<blockquote cite=\"" + cite + "\">" + item + "</blockquote>";
        }

        public static string Abbreviation(string item)
        {
            return "<abbr>" + item + "</abbr>";
        }

        public static string Address(string item)
        {
            return "<address>" + item + "</address>";
        }

        public static string Cite(string item)
        {
            return "<cite>" + item + "</cite>";
        }

        public static string BidirectionalOverride(string item)
        {
            return "<bdo>" + item + "</bdo>";
        }

        public static string Paragraph(string item)
        {
            return "<p>" + item + "</p>";
        }
    }
}
