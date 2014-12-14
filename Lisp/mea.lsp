;====================================
;  operators
;====================================


(defparameter *ops*
  (list (make-operator
         :name 'move-to-kitchen
         :pre '((in ralf cafe))
         :add '((in ralf kitchen))
         :del '((in ralf cafe)))
        (make-operator
         :name 'move-to-cafe
         :pre '((in ralf kitchen))
         :add '((in ralf cafe))
         :del '((in ralf kitchen)))
        (make-operator
         :name 'move-to-street
         :pre '((in ralf cafe))
         :add '((in ralf street))
         :del '((in ralf cafe)))
        (make-operator
         :name 'pickup-rat
         :pre '((in ralf kitchen)
                (in rat kitchen)
                (holds ralf nil))
         :add '((holds ralf rat))
         :del '((in rat kitchen)
                (holds ralf nil)))
        (make-operator
         :name 'drop-rat
         :pre '((in ralf street)
                (holds ralf rat))
         :add '((holds ralf nil)
                (in rat street))
         :del '((holds ralf rat)))))
    

;====================================
;  sample start & goal states
;====================================


(defparameter *start1*
  '((in ralf cafe)
    (in rat kitchen)
    (holds ralf nil)))

(defparameter *goal*
  '((in rat street)))

(defparameter *objects*
  '(rat))


;====================================
;  mea algorithm
;====================================


(defstruct operator
  name
  pre
  add
  del)


(defun mea (state goal operators)
  (let ((goal-stack (prioritise (get-diffrence goal state)))
        (path nil))
    (do () ((null goal-stack) path)
      (let ((next (pop goal-stack)))
        (cond ((is-operator next)
               (setf state (apply-op next state))
               (push next path))
              ((member next state)
               nil)
              (t
               (let ((op (find-op next operators)))
                 (push op goal-stack)
                 (setf goal-stack
                   (append (prioritise (preconditions-of op))
                           goal-stack)))))))))


(defun prioritise (diff-list)
  "Not implemented"
  diff-list)


#|

(defun prioritise (diff-list)
  "order goals according to their priority"
  ; for the sake of this example I have written this fn as a quick &
  ; rough fix, explore better approaches for other examples
  (sort (copy-list diff-list)
        #'(lambda (x y) (> (length ($* *objects* x))
                           (length ($* *objects* y))))
        ))

|#


(defun is-operator (op)
  (operator-p op))


(defun get-diffrence (state-1 state-2)
  ($- state-1 state-2))


(defun find-op (diff operators)
  "find an operator that responds to a difference"
  (find-if #'(lambda (op)
               (member diff (operator-add op) :test #'equal))
           operators))


(defun apply-op (op state)
  (when ($>= state (operator-pre op))
    ($+ (operator-add op) ($- state (operator-del op)))))


(defun preconditions-of (op)
  (operator-pre op))