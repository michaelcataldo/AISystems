(defparameter map
  '((A B)
    (B C E A)
    (C B F)
    (D G)
    (E B F H)
    (F C E I)
    (G D H)
    (H E G)
    (I F)))

(defun make-map-lmg (map)
  #'(lambda (room)
      (-> map room)))