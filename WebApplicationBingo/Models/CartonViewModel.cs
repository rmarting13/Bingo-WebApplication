namespace WebApplicationBingo.Models

{
    public class CartonViewModel
    {
        public int Id { get; set; }
        public List<int> ListaDeNumeros { get; set; }
        public int CantidadDeAciertos { get; set; }


        public CartonViewModel(int Id)
        {
            this.Id = Id;
            ListaDeNumeros = generarCarton();
            CantidadDeAciertos = 0;
        }

        private void _insertarNumero(List<int> col,int nroCol)
        {
            Random numAleatorio = new Random();
            int min = nroCol * 10;
            int max = min + 10;
            if (min == 0)
                min++;
            int num = numAleatorio.Next(min, max);
            while (col.Contains(num)){
                num = numAleatorio.Next(min, max);
            }
            col.Add(num);
        }

        private List<int> generarCarton()
        {
            var col = new List<int>();
            for (int i = 0; i < 9; i++)
            {   
                if(i==3 | i==6 | i==8)
                {
                    _insertarNumero(col, i);
                }
                else
                {
                    _insertarNumero(col, i);
                    _insertarNumero(col, i);
                }
                col.Sort();
            }
            return col;
        }

        public bool esGanador()
        {
            return CantidadDeAciertos >= 15;
        }
    }
}
