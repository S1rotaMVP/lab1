// <copyright file="FeedItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp1.Models
{
    /// <summary>
    /// Представляє одиницю корму в інвентарі зоопарку "ДИКИЙ СВІТ".
    /// </summary>
    public struct FeedItem
    {
        /// <summary>
        /// Gets or sets унікальний ідентифікатор корму.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets назву корму.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets ціну за один кілограм корму.
        /// </summary>
        public double PricePerKg { get; set; }

        /// <summary>
        /// Gets or sets вагу наявного корму в кілограмах на складі.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether чи є корм преміум-класу.
        /// </summary>
        /// <value>Логічне значення: true, якщо корм преміум; інакше — false.</value>
        public bool IsPremium { get; set; }
    }
}