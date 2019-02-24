; JESS aplinkoje komentarus pasalinkite
;
;(clear)

(deftemplate obstacles (slot location) (slot t_lights) (slot cars) (slot pedestrians) (slot spec_service))

(deftemplate car (slot location))

(deftemplate fragment (slot from) (slot to))

(deffacts faktu-inicializavimas
  (car (location sankryza1))
  (fragment (from sankryza1) (to sankryza2))
  (fragment (from sankryza2) (to sankryza3))
  (fragment (from sankryza3) (to sankryza4))
  (fragment (from sankryza4) (to sankryza5))
  (obstacles (location sankryza1) (t_lights red) (cars 0) (pedestrians 2) (spec_service 0))
  (obstacles (location sankryza2) (t_lights green) (cars 2) (pedestrians 0) (spec_service 0))
  (obstacles (location sankryza3) (t_lights green) (cars 0) (pedestrians 2) (spec_service 0))
  (obstacles (location sankryza4) (t_lights green) (cars 2) (pedestrians 0) (spec_service 2))
  )
  
 (defrule r1 "wait for special service"
	(declare (salience 20))
	?fact-id1 <- (car (location ?location))
	?fact-id2 <- (obstacles (location ?position) (t_lights ?t_lights) (cars ?cars) (pedestrians ?pedestrians) (spec_service ?spec_service))
	(test (eq ?location ?position))
	=>
	(if (> ?spec_service 0) then (modify ?fact-id2 (spec_service (- ?spec_service 1))))
)

(defrule r2 "wait for pedestrians"
	(declare (salience 15))
	?fact-id1 <- (car (location ?location))
	?fact-id2 <- (obstacles (location ?position) (t_lights ?t_lights) (cars ?cars) (pedestrians ?pedestrians) (spec_service ?spec_service))
	(test (eq ?location ?position))
	=>
	(if (> ?pedestrians 0) then (modify ?fact-id2 (pedestrians (- ?pedestrians 1))))
)

(defrule r3 "wait for cars"
	(declare (salience 10))
	?fact-id1 <- (car (location ?location))
	?fact-id2 <- (obstacles (location ?position) (t_lights ?t_lights) (cars ?cars) (pedestrians ?pedestrians) (spec_service ?spec_service))
	(test (eq ?location ?position))
	=>
	(if (> ?cars 0) then (modify ?fact-id2 (cars (- ?cars 1))))
)

(defrule r4 "wait for green light"
	(declare (salience 5))
	?fact-id1 <- (car (location ?location))
	?fact-id2 <- (obstacles (location ?position) (t_lights ?t_lights) (cars ?cars) (pedestrians ?pedestrians) (spec_service ?spec_service))
	(test (eq ?location ?position))
	=>
	(if (eq ?t_lights red) then (modify ?fact-id2 (t_lights green)))
)

(defrule r5 "drive"
	?fact-id1 <- (car (location ?location))
	?fact-id2 <- (fragment (from ?from) (to ?to))  
	(test (eq ?location ?from))
=>
	(if (eq ?location sankryza5) then (printout t "You have reached your destination" crlf)
									else (modify ?fact-id1 (location ?to))
									)
	)



; JESS aplinkoje komentarus pasalinkite
;
; (reset)
; (facts)
; (watch all)
; (run)