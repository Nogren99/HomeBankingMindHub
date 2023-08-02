using System;

namespace HomeBankingMindHub.dtos
{
    public class CardDTO
    {
        public long Id { get; set; }
        public string CardHolder { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Number { get; set; }
        public int Cvv { get; set; }
        //? avisa que el tipo puede aceptar un valor null
        public DateTime? FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
    }
}