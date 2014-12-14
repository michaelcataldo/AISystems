;; best first search mechanism

;; @args start start state
;; @args goal either a predicate to take a state determine if it is a goal
;;            or a state equal to the goal
;; @args LMG  legal move generator function which takes one state & returns
;;            a list of states
;; @args selector takes list of states & selects the next one to explore from

(defun breadth-search (start goal lmg
                             &key
                             (selector #'first)
                             (filter #'(lambda (x) x)))
  (let ((ready `((,start)))
        (visited nil))
    (do () ((null ready) nil)
      (let* ((next (funcall selector ready))
             (state (first next))
             (raw-state (funcall filter state)))
        (if (if* (functionp goal)
               then (funcall goal raw-state)
               else (equal goal raw-state))
            (return next))
        (setf ready (remove next ready :test #'equal))
        (when (not (member raw-state visited :test #'equal))
          (let ((moves (funcall lmg raw-state)))
            (setf ready
              (append ready
                      (mapcar #' (lambda (x) (cons x next))
                        (remove-if #'(lambda (x)
                                       (member (funcall filter x) visited))
                                   moves))))))))))


(defun test-lmg (n)
  (list (+ n 1)
        (- n 3)
        (/ n 2)))