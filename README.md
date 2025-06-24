# Tail Size Dynamics
> the fluffening

Allows you to dynamically change the size of your [Foxeline](https://gamebanana.com/mods/522004) tail based on the
statistics you accumulate, with a high degree of customization.

Each statistic has a customizable multiplier that lets you determine how much the statistic contributes to the
calculated tail size. The multipliers can be made negative by pressing Confirm.

Supported statistics include:
- Crystal Hearts
- Cassettes
- Summit Gems
- Strawberries
- Golden Strawberries
- Winged Golden Strawberries
- Moonberries
- Deaths
- Dashes
- Jumps
- Time Spent *(frames **or** seconds)*
- Map Progression *(visited rooms **or** completed maps)*

The way your tail changes size is also customizable:
- Off: The mod is disabled; your tail size remains unchanged.
- Additive: The calculated size determines your final tail size.
- Accumulative: The calculated size determines the rate by which your tail changes size per second.

The lifetime of the statistics is customizable too:
- Session: Your statistics are tracked in your current map session and are reset when you exit the map via Return to Map.
- Save Data: Your statistics are tracked cross your entire save file and are only reset when you choose to do so.

It also implements Minigame Mode, which makes you fail the map if your tail size reaches zero, with the option to
Return to Map or Restart Chapter.

Have fun fluffing around with a giant tail! *(gondola included for scale)*

![gondola-for-scale.png](Assets/gondola-for-scale.png)

> [!NOTE]
> When playing in CelesteNet, Foxeline has a client-side tail size limit that is 1.75x by default.
> This is done to prevent players with ridiculously giant tails from obstructing others' view.
> 
> You can change the limit under the "Foxeline Constants" Mod Options section, but this will only change the limit of
> the tail sizes that you receive; it will not affect the limit of other players.

## Installation & Usage

1. Install [Everest](https://everestapi.github.io/), [Celeste](https://www.celestegame.com/)'s mod loader
2. Install this mod from its GameBanana page (either via Olympus or your preferred mod manager)
3. Open Everest, go to Mod Options, and click on "Install Missing Dependencies"
   - Alternatively, install Foxeline just like in step two
4. Scroll to the mod's section to modify its settings to your liking
   - You can change the order your mods appear in by editing the `modoptionsorder.txt` file in your `Celeste/Mods` folder
5. Enter a map and enjoy!

## Building

1. Install [Everest](https://everestapi.github.io/), [Celeste](https://www.celestegame.com/)'s mod loader
2. Install [the latest .NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet)
   - Pay attention to the processor architecture; pick the right one for your computer
3. Clone the repository in your `Celeste/Mods` folder
4. Build the mod in your IDE *(using [Rider](https://www.jetbrains.com/rider) is recommended)* or via the `dotnet` CLI
   - Build in `Release` mode to generate a `.zip` file that's ready to share

## Contact

You can contact me in the `#modding_feedback` channel of the
[Mt. Celeste Climbing Association](https://discord.gg/celeste) Discord server.  
Please share feedback or bug reports there; I'd love to hear about your fluffy activities. *(really!)*
