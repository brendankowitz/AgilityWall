using System;
using PortableTrello.Contracts;

namespace AgilityWall.Core.Features.CardDetails.Designer
{
    public class DesignCardDetailsViewModel : CardDetailsViewModel
    {
        public DesignCardDetailsViewModel() : 
            base(null, null)
        {
            DisplayName = "Tart soufflé jujubes soufflé sweet candy canes. Pie biscuit macaroon.";
            Card = new Card
            {
                Desc = "Cupcake muffin bonbon. Tart jelly-o donut unerdwear.com tiramisu macaroon croissant. Pudding jelly-o brownie sweet roll. Jelly tootsie roll cheesecake candy canes cupcake lollipop topping. Donut bonbon sugar plum tootsie roll gummi bears dessert macaroon pie. Donut gummies biscuit carrot cake sugar plum. Croissant candy canes bear claw chocolate cake marshmallow jujubes cotton candy. Jelly-o gummies soufflé macaroon jelly."
            };
            List = new List
            {
                Name = "Todo list"
            };
            CoverAttachment = new Attachment
            {
                Url = "/Assets/RandomBg1.jpg"
            };
            
        }
    }
}