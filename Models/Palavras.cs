using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJogoForca.Models
{
    public class Palavras
    {
        public Palavras(string tips, string text)
        {
            this.Tips = tips;
            this.Text = text;
        }
        public string Tips { get; set; } = string.Empty; // é a Dica para a palavra
        public string Text { get; set; } = string.Empty; // é a palavra em sí
    }
}
