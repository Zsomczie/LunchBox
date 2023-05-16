This is test!
Helloooo!
-> firstQuest

=== firstQuest ===

Choose a quest.
    + [Kill 7 Broccolis]
    -> broccoli
    + [Kill 2 Cabbages]
    -> cabbage
    + [Kill 6 Carrots]
    -> carrot
    
=== broccoli ===
You chose to kill some broccolis. Have fun! #selectedQuest:broccoli
-> END

=== cabbage ===
You chose to kill some cabbages. Have fun! #selectedQuest:cabbage
-> END

=== carrot ===
You chose to kill some carrots. Have fun! #selectedQuest:carrot
-> END


=== finalChoice(target, amount) ===
You chose to kill {amount} {target}s. Have fun! #selectedQuest:

-> END