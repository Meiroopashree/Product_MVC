//new
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;
using dotnetapp.Data;
using Microsoft.EntityFrameworkCore;

public class GiftRepository
{
    private readonly ApplicationDbContext _context;

    public GiftRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Gift addGift(Gift gift)
    {
        // gift.CartId = ;
        _context.Gifts.Add(gift);
        _context.SaveChanges();
        return gift;
    }



    public List<Gift> getAllGifts()
    {
        Console.WriteLine("gift repo");
        return _context.Gifts.ToList();
    }

    // public Gift editGift(long giftId, Gift updatedGift)
    // {
    //     var existingGift = _context.Gifts.Find(giftId);

    //     if (existingGift != null)
    //     {
    //         existingGift.GiftType = updatedGift.GiftType;
    //         existingGift.GiftImageUrl = updatedGift.GiftImageUrl;
    //         existingGift.GiftDetails = updatedGift.GiftDetails;
    //         existingGift.GiftPrice = updatedGift.GiftPrice;
    //         existingGift.Quantity = updatedGift.Quantity;

    //         _context.SaveChanges();
    //         return existingGift;
    //     }

    //     return null; // Gift not found
    // }

//     public Gift editGift(long giftId, Gift updatedGift)
// {
//     var existingGift = _context.Gifts
//         .Include(g => g.Cart) // Include the associated Cart
//         .FirstOrDefault(g => g.GiftId == giftId);

//     if (existingGift != null)
//     {
//         existingGift.GiftType = updatedGift.GiftType;
//         existingGift.GiftImageUrl = updatedGift.GiftImageUrl;
//         existingGift.GiftDetails = updatedGift.GiftDetails;
//         existingGift.GiftPrice = updatedGift.GiftPrice;
//         existingGift.Quantity = updatedGift.Quantity;

//         // If the input provides a new CartId, update it
//         if (updatedGift.CartId != 0)
//         {
//             existingGift.CartId = updatedGift.CartId;
//         }
//         else
//         {
//             // Fetch the latest CartId directly from the Cart table
//             var latestCartId = _context.Carts
//                 .Where(c => c.CustomerId == existingGift.Cart.CustomerId)
//                 .OrderByDescending(c => c.CartId)
//                 .Select(c => c.CartId)
//                 .FirstOrDefault();

//             // Update the CartId in the existingGift
//             existingGift.CartId = latestCartId;
//         }

//         _context.SaveChanges();
//         return existingGift;
//     }

//     return null; // Gift not found
// }


//     public Gift editGift(long giftId, Gift updatedGift)
// {
//     var existingGift = _context.Gifts
//         .Include(g => g.Cart) // Include the associated Cart
//         .FirstOrDefault(g => g.GiftId == giftId);

//     if (existingGift != null)
//     {
//         existingGift.GiftType = updatedGift.GiftType;
//         existingGift.GiftImageUrl = updatedGift.GiftImageUrl;
//         existingGift.GiftDetails = updatedGift.GiftDetails;
//         existingGift.GiftPrice = updatedGift.GiftPrice;
//         existingGift.Quantity = updatedGift.Quantity;

//         // If the input provides a new CartId, update it
//         if (updatedGift.CartId != 0)
//         {
//             existingGift.CartId = updatedGift.CartId;
//         }

//         // Check if the associated cart exists
//         if (existingGift.Cart != null)
//         {
//             // Add the gift to the cart if it's not already present
//             if (!existingGift.Cart.Gifts.Any(g => g.GiftId == existingGift.GiftId))
//             {
//                 existingGift.Cart.Gifts.Add(existingGift);
//             }
//         }
//         else
//         {
//             // Handle scenario where the associated cart doesn't exist
//             // You might want to create a new cart for the gift or handle it differently based on your application logic
//         }

//         _context.SaveChanges();
//         return existingGift;
//     }

//     return null; // Gift not found
// }


public Gift editGift(long giftId, Gift updatedGift)
{
    var existingGift = _context.Gifts
        .Include(g => g.Cart) // Include the associated Cart
        .FirstOrDefault(g => g.GiftId == giftId);

    if (existingGift != null)
    {
        existingGift.GiftType = updatedGift.GiftType;
        existingGift.GiftImageUrl = updatedGift.GiftImageUrl;
        existingGift.GiftDetails = updatedGift.GiftDetails;
        existingGift.GiftPrice = updatedGift.GiftPrice;
        existingGift.Quantity = updatedGift.Quantity;

        // If the input provides a new CartId, update it
        if (updatedGift.CartId != 0)
        {
            existingGift.CartId = updatedGift.CartId;
        }

        // Check if the associated cart exists
        if (existingGift.Cart != null)
        {
            // Remove the gift from the cart's gifts collection and save changes
            existingGift.Cart.Gifts.Remove(existingGift);
            _context.SaveChanges();
        }

        // Add the gift back to the cart if the updated gift has a valid cart ID
        if (updatedGift.CartId != 0)
        {
            var updatedCart = _context.Carts.Include(c => c.Gifts).FirstOrDefault(c => c.CartId == updatedGift.CartId);
            if (updatedCart != null)
            {
                updatedCart.Gifts.Add(existingGift);
                _context.SaveChanges();
            }
        }

        return existingGift;
    }

    return null; // Gift not found
}




    public Gift deleteGift(long giftId)
    {
        var giftToRemove = _context.Gifts.Find(giftId);

        if (giftToRemove != null)
        {
            _context.Gifts.Remove(giftToRemove);
            _context.SaveChanges();
            return giftToRemove;
        }

        return null; // Gift not found
    }
}
