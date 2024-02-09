I have got to a point that everything is working and I have used TDD for the Majority of this, with some test cases needing fleshing out, purely because I figured that having addition and subtraction as rules could be cool.

I feel like there is more work to be done on the Converter class, especially concerning cohesion and the Parse method, feeling a bit friends of friendsy.

The RuleService also has the ValidationRule method, I did reduce the amount of if statements at one point, but it felt like it reduced readability, so decided that the seperated cases was probably better.

I decided to go for a CQRS-style setup with reguards to adding and using the rules, so that I could seperate the rule validation from the number conversion, this also allowed me to use the Composite Repostiory pattern.

I have applied some caching, but it was my first time applying it at an endpoint level, so I wouldn't be surprised if there were some issues with it.

I have decided to leave the code as is, so you can see my working process and it has been far more than 4 hours.
