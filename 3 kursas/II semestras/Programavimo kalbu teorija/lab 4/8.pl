sum([], 0) :- !.

sum(N, N) :- number(N).

sum([H|T], N) :-
sum(H, N1),
sum(T, N2),
N is N1 + N2.