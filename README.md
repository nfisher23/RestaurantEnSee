# RestaurantEnSee
RestaurantEnSee is a web application template for any single restaurant to enable online ordering and menu creation. 
Behind a secured admin page, the owner can create and remove sections and items on their menu without having to
get a fresh design each time.

## Usage
Once deployed, navigate to /RESAdmin. You will be redirected to a Login page, at which point you enter the default
username and password (username: RESEnSeeUser and password: RESDefaultPassword1!). Then you will be redirected to a
Create Account page. The details you enter there will erase the default credentials, and you can use those in the future to 
access the admin page from anywhere.

From there, the controls are quite simple. You can add and remove sections (e.g. Soups) and menu items (e.g. Black Bean Soup).
Then, you can add and remove menu items from the sections, or change various aspects of the menu items like price, name, description,
and the accompanying photograph. If you configure an email via SMTP, any user checkout will send an email to yourself indicating 
details of their order. The users can start adding items to a cart from the get go, and include special instructions on checkout.

## Improvements
Ultimately, using an email to send an order is not particularly smart. Bots from competitors, from example, could periodically spoof
the system and get the restaurant to make food that will go unpurchased. Further, not creating an account or letting people
pay online incentivizes people to say "forget it," and not come get the food. Ultimately, to be truly "production ready," this
template should include the ability for users to create an account, pay right away,
and a different mechanism for alerting the restaurant 
that something has been ordered (e.g. an app that sends a message over a secure channel).

Basically, this comes back to this web application being a template. Observe the code with that grain of salt.
