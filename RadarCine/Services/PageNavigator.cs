namespace RadarCine.Services;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public class PageNavigator(NavigationManager navigation, IJSRuntime js) {
    /// <summary>
    /// Navigates to the specified page with a transition effect.
    /// </summary>
    /// <param name="uri">Destination URI</param>
    /// <param name="direction">Transition's animation direction</param>
    /// <param name="delayMs">Delay in milliseconds before navigation occurs</param>
    public async Task NavigateWithTransitionAsync(string uri, TransitionDirection direction = TransitionDirection.Forward, int delayMs = 300) {
        await js.InvokeVoidAsync("document.body.classList.add", "page-transitioning");
        await js.InvokeVoidAsync("startPageTransition", direction.ToString().ToLower());
        
        await js.InvokeVoidAsync("sessionStorage.setItem", "pageTransitionDirection", direction.ToString());

        await Task.Delay(delayMs);
        navigation.NavigateTo(uri, forceLoad: false);

        _ = Task.Delay(300).ContinueWith(async _ => {
            await js.InvokeVoidAsync("document.body.classList.remove", "page-transitioning");
        });
    }
}

public enum TransitionDirection {
    Forward,
    Backward
}