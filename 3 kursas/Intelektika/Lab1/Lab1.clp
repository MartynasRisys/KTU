; JESS aplinkoje komentarus pasalinkite
;
;(clear)

(deftemplate pele (slot spalva) (slot kiekis) )
(deftemplate katino (slot busena) (slot suvalgyta_peliu) (slot nevalge_dienu))

(deffacts faktu-inicializavimas
  (pele (spalva pilka) (kiekis 5))
  (pele (spalva balta) (kiekis 3))
  (katino (busena "alkanas") (suvalgyta_peliu 0) (nevalge_dienu 0))
)

(defrule r1 "Kai katinas alkanas, jis nori valgyti"
  ?fact-id <- (katino (busena ?busena))  
  (test (eq ?busena "alkanas"))
  =>
  (modify ?fact-id (busena "nori valgyti"))
)
(defrule r2 "Kai katinas nori valgyti ir yra peliu, jis valgo peles"
  ?fact-id1 <- (katino (busena "nori valgyti") (suvalgyta_peliu ?suvalgyta))
  ?fact-id2 <- (pele (spalva ?spalva) (kiekis ?kiekis))
  (test (> ?kiekis 0))
  =>
  
  (if (eq ?spalva balta) then (printout t "py-py!" crlf)
                         else (printout t "pyyyyy" crlf))
  (modify ?fact-id2 (kiekis (- ?kiekis 1))  )
  
  (modify ?fact-id1 (suvalgyta_peliu (+ ?suvalgyta 1)) ) 
  (printout t "miau" crlf)
)

(defrule r3 "kai katinas suvalgo 5 peles, jis tampa storu katinu"
  (declare (salience 10))
  ?fact-id1 <- (katino (busena "nori valgyti") (suvalgyta_peliu ?suvalgyta))
  (test (= ?suvalgyta 5)) 
  
=>
  (modify ?fact-id1 (busena "storas"))
)

(defrule r4 "kai storas, nori miego"
  ?fact-id <- (katino (busena ?busena))  
  (test (eq ?busena "storas"))
  
=>
  (modify ?fact-id (busena "miega"))
)
(defrule r5 "kai pamiega, nori valgyt"
  ?fact-id <- (katino (busena ?busena) (suvalgyta_peliu ?suvalgyta))  
  (test (eq ?busena "miega"))
  
=>
  (modify ?fact-id (busena "alkanas") (suvalgyta_peliu 0))
)
(defrule r6 "Kai katinas nori valgyti ir nera peliu, jis badauja"
  ?fact-id1 <- (katino (busena "nori valgyti") (nevalge_dienu ?nevalge))
  ?fact-id2 <- (pele (spalva ?spalva) (kiekis ?kiekis))
  (test (< ?kiekis 1))
  =>
  (modify ?fact-id1 (nevalge_dienu (+ ?nevalge 1)) ) 
)
(defrule r7 "kai nevalge 7, mirsta"
	(declare (salience 10))
  ?fact-id <- (katino (busena "nori valgyti") (nevalge_dienu ?nevalge))  
  (test (= ?nevalge 7))
  
=>
  (modify ?fact-id (busena "mires"))
)

; JESS aplinkoje komentarus pasalinkite
;
; (reset)
; (facts)
; (watch all)
; (run)
