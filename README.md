# Dormoy_WS_extension

## TO INSTALL BEFORE USE:
__Install  Bing Maps Windows Presentation Foundation (WPF) Control
Version 1.0__ via: 
https://www.microsoft.com/en-us/download/details.aspx?id=27165

## ABOUT
For the extensions, I developed an improved __Winform Client__ and a new __ServiceTourism__.

## NOTES
#### Notes about the new Winform Client
I used __Bing Map WPF control__ instead of using webbrowser. This allowed to display all the segments of an itinerary.

#### Notes about ServiceTourism:

- We only compute tourism itinerary with the coordinates of the different
places. We do not take into account the opening hours because there is
no relayable data about it in the api used.

- We did not expose methods of the new ServiceTourism in REST because we
have a simple algorithm for computing the visited tourism locations. Thus,
this algorithm is not complicated enough for the REST exposure to be worth it
(as it is only for internal use).

- We consider that the user will be visiting the tourism locations by bike
(he will attach his bike to some fence, while attending the tourism location).
Even if this use case do not exactly reflect reality, we did this simplification
in order to build an example on how to build a tourism extension.
