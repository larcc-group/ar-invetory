const Objeto = require('./objeto')

Objeto.methods(['get', 'post', 'put', 'delete'])

Objeto.updateOptions({ new: true, runValidators: true })

module.exports = Objeto