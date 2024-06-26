using AppJogoForca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJogoForca.Repositories
{
    public class PalavrasRepository
    {
        private List<Palavras> _palavras;

        public PalavrasRepository() 
        {
            _palavras = new List<Palavras>();
            _palavras.Add(new Palavras("Fruta, geralmente Vermelha, mas pode ser verde!", "Maca".ToUpper()));
            _palavras.Add(new Palavras("É Fruta, mas pensam que é legume!"              , "Tomate".ToUpper()));
            _palavras.Add(new Palavras("Gostosa de Comer!"                              , "Katia".ToUpper()));
            _palavras.Add(new Palavras("Pernambuco"                                     , "Cabeca-Chata".ToUpper()));
            _palavras.Add(new Palavras("Amigo do Saci"                                  , "Curupira".ToUpper()));
            _palavras.Add(new Palavras("Melhora a saúde do coração"                     , "Azeite".ToUpper()));
            _palavras.Add(new Palavras("Pode ser doce ou salgada"                       , "Pipoca".ToUpper()));
        }

        public Palavras GetPalavrasAleatorias()
        {
            Random rand = new Random();
            var proximo = rand.Next(0, _palavras.Count);
            return _palavras[proximo];
        }
    }
}
