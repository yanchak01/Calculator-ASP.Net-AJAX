using System;

namespace Calculator.DTO
{
    public class ExampleDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Text { get; set; }
    }
}
