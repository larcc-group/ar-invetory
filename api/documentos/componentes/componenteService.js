const Componente = require('./componente')
Componente.methods(['get', 'post', 'put', 'delete'])

Componente.updateOptions({ new: true, runValidators: true })



module.exports = Componente