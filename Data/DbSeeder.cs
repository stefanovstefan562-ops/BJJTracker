using BjjTracker.Models;
using BjjTracker.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Techniques.AnyAsync())
            {
                return;
            }

            context.Techniques.AddRange(
                new Technique { TechniqueName = "Armbar", TechniquePosition = TechniquePosition.ClosedGuard, TechniqueDescription = "Isolate the arm and hyperextend the elbow." },
                new Technique { TechniqueName = "Triangle choke", TechniquePosition = TechniquePosition.ClosedGuard, TechniqueDescription = "Trap the head and one arm with your legs and squeeze." },
                new Technique { TechniqueName = "Scissor sweep", TechniquePosition = TechniquePosition.ClosedGuard, TechniqueDescription = "Sweep the opponent by scissoring your legs and pulling the arm." },
                new Technique { TechniqueName = "Tripod sweep", TechniquePosition = TechniquePosition.OpenGuard, TechniqueDescription = "Off-balance the opponent with a foot on the hip and a grip on the ankle." },
                new Technique { TechniqueName = "Knee slice pass", TechniquePosition = TechniquePosition.HalfGuard, TechniqueDescription = "Slice your knee across the thigh to pass the guard." },
                new Technique { TechniqueName = "Cross collar choke", TechniquePosition = TechniquePosition.Mount, TechniqueDescription = "Cross your hands into the collar and choke from the top." },
                new Technique { TechniqueName = "Kimura", TechniquePosition = TechniquePosition.SideControl, TechniqueDescription = "Isolate the arm and rotate it behind the back for a shoulder lock." },
                new Technique { TechniqueName = "Hip escape (shrimp)", TechniquePosition = TechniquePosition.SideControl, TechniqueDescription = "Move your hips away to create space and recover guard." },
                new Technique { TechniqueName = "Rear naked choke", TechniquePosition = TechniquePosition.BackControl, TechniqueDescription = "Wrap the neck from behind and squeeze to finish." },
                new Technique { TechniqueName = "Double leg takedown", TechniquePosition = TechniquePosition.Standing, TechniqueDescription = "Drive through the hips and take the opponent to the ground." }
            );

            await context.SaveChangesAsync();
        }
    }
}