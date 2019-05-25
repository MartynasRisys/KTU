count_not_numbers([],0).
count_not_numbers([H|Tail], N) :-
    count_not_numbers(Tail, N1),
    (number(H) -> N = N1
    ;  N is N1 + 1
    ).